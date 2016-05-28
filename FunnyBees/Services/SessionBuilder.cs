using System;
using System.Collections.Generic;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
    internal sealed class SessionBuilder : ISessionBuilder
    {
        private readonly ITimeIntervalGenerator generator;
        private readonly BeeApiarOptions options;
        private readonly IList<Beehive> beehives;

        public IBeeFactory BeeFactory
        {
            get
            {
                return 
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public SessionBuilder(ITimeIntervalGenerator generator, BeeApiarOptions options)
        {
            this.generator = generator;
            this.options = options;
            beehives = new List<Beehive>();
        }

        public void CreateBeehives(Action<IBeehiveBuilder> configurator)
        {
            for (var index = 0; index < options.NumberOfBeehives; index++)
            {
                IBeehiveBuilder temp = new BeehiveBuilder(index);

                configurator.Invoke(temp);

                beehives.Add(temp.Construct());
            }
        }

        /// <summary>
        /// Constructs target object of type <see cref="TObject" />.
        /// </summary>
        /// <returns></returns>
        Session IObjectBuilder<Session>.Construct()
        {
            return new Session(generator, beehives, options.Interval);
        }
    }
}