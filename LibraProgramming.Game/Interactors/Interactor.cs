using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LibraProgramming.Game.Components;

namespace LibraProgramming.Game.Interactors
{
    /// <inheritdoc />
    /// <summary>
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

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Interact(ComponentContainer source, ComponentContainer target)
        {
            if (false == cache.TryGetValue(GetType(), out var matches))
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

            foreach (var @interface in type.GetInterfaces())
            {
                if (false == @interface.IsConstructedGenericType)
                {
                    continue;
                }

                var definition = @interface.GetGenericTypeDefinition();

//                if (false == definition.IsAssignableFrom(typeof (IInteractor<,>)))
                if (definition != typeof (IInteractor<,>))
                {
                    continue;
                }

                var arguments = @interface.GetGenericArguments();

                if (false == IsValidInterfaceArguments(arguments))
                {
                    continue;
                }

                var method = @interface.GetTypeInfo().GetDeclaredMethod(nameof(Interact));
                var parameters = method.GetParameters()
                    .Select(parameter => Expression.Parameter(parameter.ParameterType, parameter.Name))
                    .ToArray();
                var lambda = Expression.Lambda(
                        Expression.Call(
                            Expression.Constant(this),
                            method,
                            parameters[0],
                            parameters[1]
                        ),
                        parameters)
                    .Compile();

                entries.Add(new MatchEntry(arguments, lambda));
            }

            if (false == cache.TryGetValue(type, out var matches))
            {
                matches = new InteractorMatches();
                cache.Add(type, matches);
            }

            matches.AddRange(entries);
        }

        private bool IsValidInterfaceArguments(Type[] argumentTypes)
        {
            return 2 == argumentTypes.Length && Array.TrueForAll(argumentTypes, arg => typeof (ComponentContainer).IsAssignableFrom(arg));
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