using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Simulation : ISimulation
    {
        private readonly IApplicationOptionsProvider optionsProvider;
        private readonly IBeehiveFactory beehiveFactory;
        private readonly IBeeFactory beeFactory;

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ISimulationSession> RunAsync()
        {
            var random = new Random();
            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);
            var beehives = new Collection<IBeehive>();

            foreach (var index in Enumerable.Range(1, options.NumberOfBeehives + 1))
            {
                var number = random.Next(options.MinimumNumberOfBees, options.MaximumNumberOfBees);
                var beehive = beehiveFactory.GetBeehive(index, number);

                beehives.Add(beehive);

                foreach (var num in Enumerable.Range(1, beehive.MaximumNumberOfBees + 1))
                {
                    var bee = beeFactory.CreateBee(num);

                    beehive.Bees.Add(bee);
                    bee.Beehive = beehive;
                }
            }

            return new SimulationSession(beehives, options.Interval, Cleanup);
        }

        private void Cleanup()
        {
        }
    }
}