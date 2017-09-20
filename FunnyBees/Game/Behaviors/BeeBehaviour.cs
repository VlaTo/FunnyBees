using LibraProgramming.Game.Components;

namespace FunnyBees.Game.Components
{
    public abstract class BeeBehaviour : Component<Bee>
    {
        public abstract void Die();
    }
}