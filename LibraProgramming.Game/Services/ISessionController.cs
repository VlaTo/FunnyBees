using System;
using Windows.Foundation;
using LibraProgramming.FunnyBees.Models;

namespace LibraProgramming.FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionStartingEventArgs : EventArgs
    {
        public bool Cancel
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISessionController
    {
        event TypedEventHandler<ISessionController, SessionStartingEventArgs> SessionStarting;

        /// <summary>
        /// 
        /// </summary>
        Session Create(BeeApiarOptions options, Action<ISessionBuilder> configurator);
    }
}