using System;

namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObserver"></typeparam>
    public interface IObservable<in TObserver>
        where TObserver : IObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable Subscribe(TObserver observer);
    }
}