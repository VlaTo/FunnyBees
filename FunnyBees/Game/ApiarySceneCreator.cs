using System;
using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Engine;
using FunnyBees.Game.Components;
using FunnyBees.Game.Interactors;
using FunnyBees.Services;

namespace FunnyBees.Game
{
    public sealed class ApiarySceneCreator : ISceneCreator
    {
        private readonly IApplicationOptionsProvider optionsProvider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ApiarySceneCreator(IApplicationOptionsProvider optionsProvider)
        {
            this.optionsProvider = optionsProvider;
        }

        public async Task CreateScene(Scene scene)
        {
//            const int guardiansCount = 3;

//            var random = new Random();
            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);

            for (var position = 0; position < options.BeehivesCount; position++)
            {
                var beehive = new Beehive();

                beehive.AddComponent(new BeeManager(options.BeehiveCapacity));

                scene.AddChild(beehive);

                var queen = new Bee();

                queen.AddComponent(new BeeLifetime(TimeSpan.FromSeconds(3.0d)));
                queen.AddComponent(new QueenBee(beehive));
                queen.InteractWith(beehive).Using<BeeHoster>();

                scene.AddChild(queen);

                /*var beesCount = random.Next(guardiansCount + 1 + 1, options.BeehiveCapacity);
                var currentCount = 0;

                // добавим пчелу-матку
                if (currentCount < 1)
                {
                    var queen = new Bee();

                    queen.AddComponent(new BeeLifetime(TimeSpan.MaxValue));
                    queen.AddComponent(new QueenBee(beehive));
                    queen.AddComponent<HomeBeehive>();
                    queen.InteractWith(beehive).Using<BeeAssigner>();

                    scene.Children.Add(queen);

                    currentCount++;
                }

                // добавим пчёл-"охранников"
                for (var index = 0; index < guardiansCount && currentCount < beesCount; index++, currentCount++)
                {
                    var guard = new Bee();

                    guard.AddComponent(new BeeLifetime(TimeSpan.FromSeconds(5.0d)));
                    guard.AddComponent(new GuardBee(beehive));
                    guard.AddComponent<HomeBeehive>();
                    guard.InteractWith(beehive).Using<BeeAssigner>();

                    scene.Children.Add(guard);
                }

                // добавим рабочих пчёл
                while (currentCount++ < beesCount)
                {
                    var bee = new Bee();

                    bee.AddComponent(new BeeLifetime(TimeSpan.FromMinutes(1.0d)));
                    bee.AddComponent(new WorkBee(beehive));
                    bee.AddComponent<HomeBeehive>();
                    bee.InteractWith(beehive).Using<BeeAssigner>();

                    scene.Children.Add(bee);
                }*/
            }
        }
    }
}