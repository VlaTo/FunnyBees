namespace FunnyBees.Models
{
    public class BeehiveFactory : IBeehiveFactory
    {
        public Beehive GetBeehive(int index, int maximumNumberOfBees)
        {
            return new Beehive(index, maximumNumberOfBees);
        }
    }
}