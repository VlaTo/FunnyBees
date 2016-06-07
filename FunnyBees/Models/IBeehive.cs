using System.Collections.Generic;

namespace FunnyBees.Models
{
    public interface IBeehive : IUpdatable<UpdateContext>
    {
        FuzzyValue<int> NumberOfBees
        {
            get;
        }

        IList<IBee> Bees
        {
            get;
        }

        int Number
        {
            get;
            set;
        } 
    }
}