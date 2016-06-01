using System;
using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    public class QueenBeeBehaviour : BeeBehaviour
    {
        public static EntityProperty LastUpdated = EntityProperty.Create();
        public static TimeSpan Timeout = TimeSpan.FromMilliseconds(100.0d);

        public override void Update(IBee bee, ISessionContext context)
        {
            var elapsed = context.Elapsed;
            var lastUpdated = elapsed;

            if (false == bee.PropertyExists(LastUpdated))
            {
                bee[LastUpdated] = elapsed;
            }
            else
            {
                lastUpdated = (TimeSpan) bee[LastUpdated];
            }

            var timeout = elapsed - lastUpdated;

            if (timeout >= Timeout)
            {
                var beehive = context.GetBeehive(bee.NativeBeehiveIndex);
                var temp = new Bee(bee.NativeBeehiveIndex, new WorkingBeeBehavoiur());

                beehive.Bees.Add(temp);
                temp[LastUpdated] = elapsed;
            }
        }
    }
}