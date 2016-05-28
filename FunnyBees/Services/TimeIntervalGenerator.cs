using System;
using System.Threading;
using Windows.Foundation;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{

    public sealed class TimeIntervalGenerator : ITimeIntervalGenerator
    {
        private readonly TypedWeakEventHandler<ITimeIntervalGenerator, TimeIntervalEventArgs> timeInterval;
        private Timer timer;

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<ITimeIntervalGenerator, TimeIntervalEventArgs> TimeInterval
        {
            add
            {
                timeInterval.AddHandler(value);
            }
            remove
            {
                timeInterval.RemoveHandler(value);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public TimeIntervalGenerator()
        {
            timeInterval = new TypedWeakEventHandler<ITimeIntervalGenerator, TimeIntervalEventArgs>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDisposable Start(TimeSpan interval)
        {
            if (null == timer)
            {
                timer = new Timer(DoTimerCallback, null, TimeSpan.Zero, interval);
            }

            return new DisposableToken<TimeIntervalGenerator>(this, DoCleanup);
        }

        private void DoCleanup(TimeIntervalGenerator generator)
        {
            if (null == timer)
            {
                return;
            }

            timer.Dispose();
            timer = null;
        }

        private void DoTimerCallback(object state)
        {
            timeInterval.Invoke(this, new TimeIntervalEventArgs());
        }
    }
}