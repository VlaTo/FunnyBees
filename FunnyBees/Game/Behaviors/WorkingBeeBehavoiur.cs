using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    public class WorkingBeeBehavoiur : BeeBehaviour
    {
        public static EntityProperty Lifespan = EntityProperty.Create();

        public override void Update(IBee bee, ISessionContext context)
        {
            var elapsed = context.Elapsed;
        }
    }
}