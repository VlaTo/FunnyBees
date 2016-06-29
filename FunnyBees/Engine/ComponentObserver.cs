using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FunnyBees.Engine
{
    public class ComponentObserver : Component, IComponentObserver
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
            subscription = ((IObservable<IComponentObserver>) Container).Subscribe(this);
        }

        protected override void OnDetach()
        {
            subscription.Dispose();
        }

        void IComponentObserver.OnAttached(IComponent component)
        {
            ImplementorEntry entry;

            if (false == cache.TryGetValue(GetType(), out entry))
            {
                throw new InvalidOperationException();
            }

            var observer = entry.FindObserver(component.GetType());

            if (null == observer)
            {
                return;
            }

            observer.AttachMethod.DynamicInvoke(component);
        }

        void IComponentObserver.OnDetached(IComponent component)
        {

        }

        private void RegisterObserverEntries(Type type)
        {
            lock (((ICollection) cache).SyncRoot)
            {
                if (cache.ContainsKey(type))
                {
                    throw new Exception();
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
            return 1 == arguments.Length && arguments[0].IsAssignableFrom(typeof (IComponent));
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

            public static ObserverEntry CreateFrom(ComponentObserver @this, Type type)
            {
                var method = type.GetTypeInfo().GetDeclaredMethod("OnAttached");
                var parameters = method.GetParameters()
                    .Select(parameter => Expression.Parameter(parameter.ParameterType, parameter.Name))
                    .ToArray();
                var onattached = Expression.Lambda(
                    Expression.Call(
                        Expression.Constant(@this),
                        method,
                        parameters[0]
                        ),
                    parameters)
                    .Compile();
                new ObserverEntry
                {
                    AttachMethod = onattached
                };
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