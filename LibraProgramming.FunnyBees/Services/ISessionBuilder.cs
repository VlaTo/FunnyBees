using System;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.FunnyBees.Services
{
    public interface ISessionBuilder : IObjectBuilder<Session>
    {
        IBeeFactory BeeFactory
        {
            get;
        }

        void CreateBeehives(Action<IBeehiveBuilder> configurator);
    }
}