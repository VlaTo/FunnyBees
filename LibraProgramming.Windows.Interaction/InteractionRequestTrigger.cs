using LibraProgramming.Windows.Interactivity;

namespace LibraProgramming.Windows.Interaction
{
    public class InteractionRequestTrigger : EventTrigger
    {
        protected override string GetEventName()
        {
            return "Raised";
        }
    }
}