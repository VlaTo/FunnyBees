using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Services;
using LibraProgramming.Windows;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Simulation : ISimulation
    {
        private readonly IApplicationOptionsProvider optionsProvider;
        private Timer timer;
        private readonly IBeehiveFactory beehiveFactory;
        private readonly IBeeFactory beeFactory;
        private readonly IList<IBeehive> beehives;
        private DateTime started;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Simulation(
            IApplicationOptionsProvider optionsProvider, 
            IBeehiveFactory beehiveFactory,
            IBeeFactory beeFactory
            )
        {
            this.optionsProvider = optionsProvider;
            this.beehiveFactory = beehiveFactory;
            this.beeFactory = beeFactory;

            beehives = new List<IBeehive>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IRunSimulationToken> RunAsync()
        {
//            var random = new Random();
            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);

            foreach (var index in Enumerable.Range(0, options.NumberOfBeehives))
            {
                var beehive = beehiveFactory.GetBeehive();

                beehive.Number = index;

                beehives.Add(beehive);

                foreach (var number in Enumerable.Range(0, beehive.MaximumNumberOfBees))
                {
                    var bee = beeFactory.CreateBee();

                    beehive.Bees.Add(bee);
                    bee.Beehive = beehive;
                }
            }

            started = DateTime.Now;
            timer = new Timer(DoTimerCallback, null, TimeSpan.Zero, options.Interval);

            return new RunSimulationToken(Cleanup);
        }

        private void Cleanup()
        {
            timer.Dispose();
            timer = null;
        }

        private void DoTimerCallback(object state)
        {
            var now = DateTime.Now;
            var context = new UpdateContext(now - started);

            foreach (var entity in beehives)
            {
                entity.Update(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class RunSimulationToken : DisposableToken, IRunSimulationToken
        {
            public RunSimulationToken(Action cleanup)
                : base(cleanup)
            {
            }
        }
    }
}