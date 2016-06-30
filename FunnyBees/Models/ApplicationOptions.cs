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
        public int BeehivesCount
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
        public int BeehiveCapacity
        {
            get;
            set;
        }
    }
}