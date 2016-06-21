namespace FunnyBees.Models
{
    public interface IBeeFactory
    {
        IBee CreateBee(int index);
    }
}