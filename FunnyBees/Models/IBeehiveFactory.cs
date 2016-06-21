namespace FunnyBees.Models
{
    public interface IBeehiveFactory
    {
        Beehive GetBeehive(int index, int maximumNumberOfBees);
    }
}