using System;

namespace FunnyBees.Game.Components
{
    public class QueenBeeBehaviour : BeeBehaviour
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(1.0d);

        private TimeSpan lastChecked;

        public QueenBeeBehaviour()
        {
            lastChecked = TimeSpan.Zero;
        }

        public override void Update(TimeSpan elapsed)
        {
            if (TimeSpan.Zero == lastChecked)
            {
                lastChecked = elapsed;
            }
            else if (Period < (elapsed - lastChecked))
            {
                var beehive = Container.Beehive;

                foreach (var bee in beehive.GetComponent<BeeManager>().Bees)
                {
                    var worker = bee.GetComponent<WorkingBeeBehaviour>(failIfNotExists: false);

                    if (null == worker)
                    {
                        continue;
                    }

                    worker.Search();
                }
                
                lastChecked = elapsed;
            }
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }
    }
}