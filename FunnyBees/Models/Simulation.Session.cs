using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Windows.Foundation;
using LibraProgramming.Windows;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Models
{
    partial class Simulation
    {
        /// <summary>
        /// 
        /// </summary>
        private class SimulationSession : DisposableToken, ISimulationSession
        {
            private bool disposed;
            private readonly Timer timer;
            private readonly DateTime started;
            private readonly TypedWeakEventHandler<ISimulationSession, SessionUpdatedEventArgs> updated;

            /// <summary>
            /// 
            /// </summary>
            public ICollection<IBeehive> Beehives
            {
                get;
            }

            /// <summary>
            /// 
            /// </summary>
            public event TypedEventHandler<ISimulationSession, SessionUpdatedEventArgs> Updated
            {
                add
                {
                    updated.AddHandler(value);
                }
                remove
                {
                    updated.RemoveHandler(value);
                }
            }

            public SimulationSession(Collection<IBeehive> beehives, TimeSpan updateInterval, Action cleanup)
                : base(cleanup)
            {
                started = DateTime.Now;
                Beehives = beehives;
                updated = new TypedWeakEventHandler<ISimulationSession, SessionUpdatedEventArgs>();
                timer = new Timer(DoTimerCallback, null, TimeSpan.Zero, updateInterval);
            }

            public override void Dispose()
            {
                Dispose(true);
            }

            private void DoTimerCallback(object state)
            {
                var now = DateTime.Now;
                var context = new UpdateContext(now - started);

                foreach (var beehive in Beehives)
                {
                    beehive.Update(context);
                }

                updated.Invoke(this, new SessionUpdatedEventArgs(context.Elapsed));
            }

            private void Dispose(bool dispose)
            {
                if (disposed)
                {
                    return;
                }

                try
                {
                    if (dispose)
                    {
                        timer.Dispose();
                        Beehives.Clear();

                        base.Dispose();
                    }
                }
                finally
                {
                    disposed = true;
                }
            }
        }
    }
}