using FunnyBees.Engine;
using FunnyBees.Game.Components;

namespace FunnyBees.Game.Interactors
{
    public class BeeHost : Interactor, IInteractor<Bee, Beehive>
    {
        /// <summary>
        /// Пчела прилетела в улей.
        /// </summary>
        /// <param name="bee"></param>
        /// <param name="beehive"></param>
        public void Interact(Bee bee, Beehive beehive)
        {
            var connector = beehive.GetComponent<BeesOwner>();

            connector.AddBee(bee);
        }
    }
}