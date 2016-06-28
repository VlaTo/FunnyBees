using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Interactor : IInteractor
    {
        private static readonly IDictionary<Type, InteractorMatches> cache;
         
        protected Interactor()
        {
            RegisterInteractionEntries(GetType());
        }

        static Interactor()
        {
            cache = new Dictionary<Type, InteractorMatches>();
        }

        public void Interact(ComponentContainer source, ComponentContainer target)
        {
            InteractorMatches matches;

            if (false == cache.TryGetValue(GetType(), out matches))
            {
                throw new InvalidOperationException();
            }

            var args = new[]
            {
                source.GetType(), target.GetType()
            };

            var entry = matches.FindEntry(args);

            if (null == entry)
            {
                throw new InvalidOperationException();
            }

            entry.InteractMethod.DynamicInvoke(source, target);
        }

        private void RegisterInteractionEntries(Type type)
        {
            var entries = new Collection<MatchEntry>();

            foreach (var intf in type.GetInterfaces())
            {
                if (false == intf.IsConstructedGenericType || false == typeof (IInteractor).IsAssignableFrom(type))
                {
                    continue;
                }

                var args = intf.GetGenericArguments();

                if (2 != args.Length)
                {
                    continue;
                }

                if (Array.TrueForAll(args, arg => typeof (ComponentContainer).IsAssignableFrom(arg)))
                {
                    var method = intf.GetRuntimeMethod("Interact", args);

                    if (null == method)
                    {
                        throw new Exception();
                    }

                    var arg0 = Expression.Parameter(args[0]);
                    var arg1 = Expression.Parameter(args[1]);
                    var temp = Expression.Lambda(
                        Expression.Call(
                            Expression.Constant(this),
                            method,
                            arg0,
                            arg1
                            ),
                        arg0,
                        arg1)
                        .Compile();

                    entries.Add(new MatchEntry(args, temp));
                }
            }

            InteractorMatches matches;

            if (false == cache.TryGetValue(type, out matches))
            {
                matches = new InteractorMatches();
                cache.Add(type, matches);
            }

            matches.AddRange(entries);
        }

        /// <summary>
        /// 
        /// </summary>
        private class InteractorMatches
        {
            private readonly ICollection<MatchEntry> entries;

            public InteractorMatches()
            {
                entries = new Collection<MatchEntry>();
            }

            public void AddRange(IEnumerable<MatchEntry> items)
            {
                foreach (var item in items)
                {
                    entries.Add(item);
                }
            }

            public MatchEntry FindEntry(Type[] types)
            {
                return entries.FirstOrDefault(entry => entry.Match(types));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class MatchEntry
        {
            private readonly Type[] types;

            public Delegate InteractMethod
            {
                get;
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public MatchEntry(Type[] types, Delegate interactMethod)
            {
                this.types = types;
                InteractMethod = interactMethod;
            }

            public bool Match(Type[] args)
            {
                if (null == args)
                {
                    return false;
                }

                if (ReferenceEquals(types, args))
                {
                    return true;
                }

                if (types.Length != args.Length)
                {
                    return false;
                }

                for (var index = 0; index < types.Length; index++)
                {
                    if (types[index] == args[index])
                    {
                        continue;
                    }

                    return false;
                }

                return true;
            }
        }
    }
}