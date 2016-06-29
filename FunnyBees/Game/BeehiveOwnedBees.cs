using System.Collections.Generic;
using FunnyBees.Engine;

namespace FunnyBees.Game
{
    public class BeehiveOwnedBees : Component
    {
        public IList<Bee> Bees
        {
            get;
        }

        public BeehiveOwnedBees()
        {
            Bees = new List<Bee>();
        }
    }
}