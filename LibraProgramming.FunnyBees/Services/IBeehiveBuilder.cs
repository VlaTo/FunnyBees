using System;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.FunnyBees.Services
{
    public interface IBeehiveBuilder : IObjectBuilder<Beehive>
    {
        IBeehiveBuilder AddBee(Action<IBeeBuilder> configurator);
    }
}