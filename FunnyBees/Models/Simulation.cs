using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Services;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Simulation : ISimulation
    {
        private readonly IApplicationOptionsProvider optionsProvider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Simulation(IApplicationOptionsProvider optionsProvider)
        {
            this.optionsProvider = optionsProvider;
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

            foreach (var index in Enumerable.Range(1, options.BeehivesCount + 1))
            {
                var maximumNumberOfBees = random.Next(options.MinimumNumberOfBees, options.BeehiveCapacity);
                var beehive = new Beehive(index, maximumNumberOfBees);
                var hasQueen = false;

                beehives.Add(beehive);

                foreach (var num in Enumerable.Range(0, beehive.MaximumNumberOfBees))
                {
                    IBeeBehaviour behaviour;

                    if (hasQueen)
                    {
                        var lifetime = random.Next(100, 500);
                        behaviour = new WorkingBeeBehaviour(lifetime);
                    }
                    else
                    {
                        behaviour = new QueenBeeBehaviour();
                        hasQueen = true;
                    }

                    var bee = new Bee(beehive, num, behaviour);

                    beehive.AddBee(bee);
                }
            }

            return new SimulationSession(beehives, options.Interval, Cleanup);
        }

        private void Cleanup()
        {
        }
    }
}