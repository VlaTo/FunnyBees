namespace FunnyBees.Engine
{
    public class BeeHost : Interactor, IInteractor<Bee, Beehive>
    {
        public void Interact(Bee bee, Beehive beehive)
        {
            var connector = beehive.GetComponent<BeehiveOwnedBees>();

            connector.Bees.Add(bee);
        }
    }
}