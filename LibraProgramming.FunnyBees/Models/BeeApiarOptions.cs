using System;

namespace LibraProgramming.FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeeApiarOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Interval
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfBeehives
        {
            get;
            set;
        }
    }
}