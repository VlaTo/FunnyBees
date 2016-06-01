using System;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.FunnyBees.Services
{
    public interface ISessionBuilder : IObjectBuilder<Session>
    {
        void CreateBeehive(Action<IBeehiveBuilder> configurator);
    }
}