using LibraProgramming.Game.Interactors;

namespace FunnyBees.Game.Interactors
{
    /// <summary>
    /// </summary>
    public class BeeHoster : Interactor, IInteractor<Bee, Beehive>
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="bee"></param>
        /// <param name="beehive"></param>
        public void Interact(Bee bee, Beehive beehive)
        {
            beehive.GetComponent<BeeManager>().AddBee(bee);
        }
    }
}