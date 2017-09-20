using System;

namespace LibraProgramming.FunnyBees.Models
{
    public interface ISessionContext : IContext
    {
        TimeSpan Elapsed
        {
            get;
        }

        Beehive GetBeehive(int index);
    }
}