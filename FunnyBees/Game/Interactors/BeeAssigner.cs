using FunnyBees.Engine;
using FunnyBees.Game.Components;

namespace FunnyBees.Game.Interactors
{
    public class BeeAssigner : Interactor, IInteractor<Bee, Beehive>
    {
        /// <summary>
        /// </summary>
        /// <param name="bee"></param>
        /// <param name="beehive"></param>
        public void Interact(Bee bee, Beehive beehive)
        {
            var home = bee.GetComponent<BeehiveReference>();
            home.Beehive = beehive;
        }
    }
}