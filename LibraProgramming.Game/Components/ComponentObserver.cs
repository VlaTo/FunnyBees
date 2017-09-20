using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LibraProgramming.Game.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    public class ComponentObserver<TContainer> : Component<TContainer>, IComponentObserver
        where TContainer : ComponentContainer
    {
        private static readonly IDictionary<Type, ImplementorEntry> cache;

        private IDisposable subscription;

        protected ComponentObserver()
        {
            RegisterObserverEntries(GetType());
        }

        static ComponentObserver()
        {
            cache = new Dictionary<Type, ImplementorEntry>();
        }

        protected override void OnAttach()
        {
            subscription = ((Engine.IObservable<IComponentObserver>) Container).Subscribe(this);
        }

        protected override void OnDetach()
        {
            subscription.Dispose();
        }

        void IComponentObserver.OnAttached(IComponent component)
        {
            var observer = GetObserverEntry(component);
            observer.AttachMethod.DynamicInvoke(component);
        }

        void IComponentObserver.OnDetached(IComponent component)
        {
            var observer = GetObserverEntry(component);
            observer.DetachMethod.DynamicInvoke(component);
        }

        private ObserverEntry GetObserverEntry(IComponent component)
        {
            lock (((ICollection) cache).SyncRoot)
            {
                ImplementorEntry entry;

                if (false == cache.TryGetValue(GetType(), out entry))
                {
                    throw new InvalidOperationException();
                }

                return entry.FindObserver(component.GetType());
            }
        }

        private void RegisterObserverEntries(Type type)
        {
            lock (((ICollection) cache).SyncRoot)
            {
                if (cache.ContainsKey(type))
                {
                    return;
                }

                var entries = new HashSet<Tuple<Type, ObserverEntry>>();

                foreach (var @interface in type.GetInterfaces())
                {
                    if (false == IsValidInterface(@interface))
                    {
                        continue;
                    }

                    var arguments = @interface.GetGenericArguments();

                    if (false == IsValidArguments(arguments))
                    {
                        throw new Exception();
                    }

                    entries.Add(new Tuple<Type, ObserverEntry>(arguments[0], ObserverEntry.CreateFrom(this, @interface)));

                }

                cache.Add(type, new ImplementorEntry(entries));
            }
        }

        private static bool IsValidInterface(Type @interface)
        {
            if (false == @interface.IsConstructedGenericType)
            {
                return false;
            }

            var definition = @interface.GetGenericTypeDefinition();

            return typeof (IComponentObserver<>) == definition;
        }

        private static bool IsValidArguments(Type[] arguments)
        {
            return 1 == arguments.Length && typeof (IComponent).IsAssignableFrom(arguments[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        private class ObserverEntry
        {
            public Delegate AttachMethod
            {
                get;
            }

            public Delegate DetachMethod
            {
                get;
            }

            private ObserverEntry(Delegate attachMethod, Delegate detachMethod)
            {
                AttachMethod = attachMethod;
                DetachMethod = detachMethod;
            }

            public static ObserverEntry CreateFrom(ComponentObserver<TContainer> @this, Type type)
            {
                var typeinfo = type.GetTypeInfo();
                var onattached = CreateMethodDelegate(@this, typeinfo, nameof(IComponentObserver.OnAttached));
                var ondetached = CreateMethodDelegate(@this, typeinfo, nameof(IComponentObserver.OnDetached));

                return new ObserverEntry(onattached, ondetached);
            }

            private static Delegate CreateMethodDelegate(ComponentObserver<TContainer> @this, TypeInfo typeinfo,
                string methodName)
            {
                var method = typeinfo.GetDeclaredMethod(methodName);
                var parameters = method.GetParameters()
                    .Select(parameter => Expression.Parameter(parameter.ParameterType, parameter.Name))
                    .ToArray();
                return Expression
                    .Lambda(
                        Expression
                            .Call(
                                Expression.Constant(@this),
                                method,
                                parameters[0]
                            ),
                        parameters)
                    .Compile();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class ImplementorEntry
        {
            private readonly IDictionary<Type, ObserverEntry> observers;

            public ImplementorEntry(IEnumerable<Tuple<Type, ObserverEntry>> source)
            {
                observers = new Dictionary<Type, ObserverEntry>();

                foreach (var tuple in source)
                {
                    observers.Add(tuple.Item1, tuple.Item2);
                }
            }

            public ObserverEntry FindObserver(Type componentType)
            {
                ObserverEntry entry;

                if (observers.TryGetValue(componentType, out entry))
                {
                    return entry;
                }

                return null;
            }
        }
    }
}