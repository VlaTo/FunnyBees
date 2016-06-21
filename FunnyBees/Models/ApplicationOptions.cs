using System;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationOptions
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

        /// <summary>
        /// 
        /// </summary>
        public int MinimumNumberOfBees
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumNumberOfBees
        {
            get;
            set;
        }
    }
}