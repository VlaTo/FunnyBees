using System;
using System.Collections.Generic;
using System.Linq;
using LibraProgramming.FunnyBees.Interop;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
    internal sealed class SessionBuilder : ISessionBuilder
    {
        private readonly ITimeIntervalGenerator generator;
        private readonly TimeSpan interval;
        private readonly IList<IEntity> beehives;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public SessionBuilder(ITimeIntervalGenerator generator, TimeSpan interval)
        {
            this.generator = generator;
            this.interval = interval;
            beehives = new List<IEntity>();
        }

        public void CreateBeehive(Action<IBeehiveBuilder> configurator)
        {
            var count = beehives.OfType<Beehive>().Count();
            IBeehiveBuilder builder = new BeehiveBuilder(count);

            configurator.Invoke(builder);

            beehives.Add(builder.Construct());
        }

        /// <summary>
        /// Constructs target object of type <see cref="TObject" />.
        /// </summary>
        /// <returns></returns>
        Session IObjectBuilder<Session>.Construct()
        {
            return new Session(generator, beehives, interval);
        }
    }
}