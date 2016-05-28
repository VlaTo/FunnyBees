using System;

namespace FunnyBees.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class RuntimeDataCache<TValue> where TValue : class
    {
        private readonly TimeSpan timeout;
        private DateTime setupTime;
        private bool assigned;
        private TValue item;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public RuntimeDataCache(TimeSpan timeout)
        {
            this.timeout = timeout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Set(TValue value)
        {
            item = value;
            setupTime = DateTime.Now;
            assigned = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(out TValue value)
        {
            value = default(TValue);

            if (!assigned)
            {
                return false;
            }

            if (timeout > TimeSpan.Zero)
            {
                var elapsed = DateTime.Now - setupTime;

                if (elapsed > timeout)
                {
                    return false;
                }
            }

            value = item;

            return true;
        }
    }
}