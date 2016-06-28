using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FunnyBees.Engine
{
    public class Interactor : IInteractor
    {
        private readonly InteractorEntry[] matches;
         
        protected Interactor()
        {
            matches = BuildEntries(GetType());
        }

        public void Interact(ComponentContainer source, ComponentContainer target)
        {
            var args = new[]
            {
                source.GetType(), target.GetType()
            };

            var entry = Array.Find(matches, item => item.CanHandle(args));

            if (null == entry)
            {
                throw new InvalidOperationException();
            }

            entry.InteractMethod.DynamicInvoke(source, target);
        }

        private InteractorEntry[] BuildEntries(Type type)
        {
            var collection = new Collection<InteractorEntry>();

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
                    collection.Add(new InteractorEntry(args, temp));
                }
            }

            return collection.ToArray();
        }

        private class InteractorEntry
        {
            private readonly Type[] types;

            public Delegate InteractMethod
            {
                get;
            }

            public InteractorEntry(Type[] types, Delegate @delegate)
            {
                this.types = types;
                InteractMethod = @delegate;
            }

            public bool CanHandle(Type[] args)
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