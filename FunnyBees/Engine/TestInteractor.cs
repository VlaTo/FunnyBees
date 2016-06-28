namespace FunnyBees.Engine
{
    public class TestInteractor : Interactor, IInteractor<Bee, Beehive>
    {
        public void Interact(Bee bee, Beehive beehive)
        {
            var connector = bee.GetComponent<BeehiveConnector>();

            connector.
        }
    }
}