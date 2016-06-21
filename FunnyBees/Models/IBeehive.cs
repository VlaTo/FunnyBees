using System.Collections.Generic;

namespace FunnyBees.Models
{
    public interface IBeehive : IUpdatable<UpdateContext>
    {
        IList<IBee> Bees
        {
            get;
        }

        int Number
        {
            get;
        } 
    }
}