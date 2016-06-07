using System;
using Windows.Foundation;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
/*
    /// <summary>
    /// 
    /// </summary>
    public class SessionController : ISessionController
    {
        private readonly TypedWeakEventHandler<ISessionController, SessionStartingEventArgs> sessionStarting;
        private readonly ITimeIntervalGenerator generator;

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<ISessionController, SessionStartingEventArgs> SessionStarting
        {
            add
            {
                sessionStarting.AddHandler(value);
            }
            remove
            {
                sessionStarting.RemoveHandler(value);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public SessionController(ITimeIntervalGenerator generator)
        {
            this.generator = generator;
            sessionStarting = new TypedWeakEventHandler<ISessionController, SessionStartingEventArgs>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Session Create(BeeApiarOptions options, Action<ISessionBuilder> configurator)
        {
            if (false == CanStart())
            {
                throw new InvalidOperationException();
            }

            ISessionBuilder builder = new SessionBuilder(generator, options.Interval);

            configurator.Invoke(builder);

            return builder.Construct();
        }

        private bool CanStart()
        {
            var args = new SessionStartingEventArgs();

            sessionStarting.Invoke(this, args);

            return false == args.Cancel;
        }
    }
*/
}