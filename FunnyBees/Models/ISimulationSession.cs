using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SessionUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Elapsed
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsed"></param>
        public SessionUpdatedEventArgs(TimeSpan elapsed)
        {
            Elapsed = elapsed;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISimulationSession : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        ICollection<IBeehive> Beehives
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        event TypedEventHandler<ISimulationSession, SessionUpdatedEventArgs> Updated;
    }
}