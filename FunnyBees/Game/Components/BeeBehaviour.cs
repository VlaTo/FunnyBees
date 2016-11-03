using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public abstract class BeeBehaviour : Component<Bee>
    {
        public abstract void Die();
    }
}