using System;
using System.Reflection;

namespace LibraProgramming.Windows.Infrastructure
{
    public class WeakDelegateBase
    {
        protected readonly WeakReference Reference;
        protected readonly MethodInfo Method;

        public bool IsAlive => null != Reference && Reference.IsAlive;

        public WeakDelegateBase(Delegate @delegate)
        {
            if (null == @delegate)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (null != @delegate.Target)
            {
                Reference = new WeakReference(@delegate.Target);
            }

            Method = @delegate.GetMethodInfo();
        }

        public bool Equals(Delegate other)
        {
            return null != other && Reference.Target == other.Target && Method.Equals(other.GetMethodInfo());
        }

        protected Delegate CreateDelegate<TDelegate>()
        {
            if (Method.IsStatic)
            {
                return Method.CreateDelegate(typeof(TDelegate));
            }

            if (null == Reference)
            {
                throw new InvalidOperationException();
            }

            return Method.CreateDelegate(typeof(TDelegate), Reference.Target);
        }
    }
}