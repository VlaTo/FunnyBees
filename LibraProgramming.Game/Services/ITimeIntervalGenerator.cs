using System;
using Windows.Foundation;

namespace LibraProgramming.FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeIntervalEventArgs : EventArgs
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITimeIntervalGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        event TypedEventHandler<ITimeIntervalGenerator, TimeIntervalEventArgs> TimeInterval;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDisposable Start(TimeSpan interval);
    }
}