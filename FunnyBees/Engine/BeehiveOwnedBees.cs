using System.Collections.Generic;

namespace FunnyBees.Engine
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