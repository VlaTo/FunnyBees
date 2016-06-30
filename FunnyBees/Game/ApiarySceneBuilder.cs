﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Engine;
using FunnyBees.Game.Components;
using FunnyBees.Services;

namespace FunnyBees.Game
{
    public sealed class ApiarySceneBuilder : ISceneBuilder
    {
        private readonly IApplicationOptionsProvider optionsProvider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ApiarySceneBuilder(IApplicationOptionsProvider optionsProvider)
        {
            this.optionsProvider = optionsProvider;
        }

        public async Task CreateScene(Scene scene)
        {
            var random = new Random();
            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);

            for (var position = 0; position < options.BeehivesCount; position++)
            {
                var beehive = new Beehive();
                var owner = new BeesOwner();

                beehive.AddComponent(owner);
                beehive.AddComponent(new BeeProducer(options.BeehiveCapacity));

                scene.AddObject(beehive);

                var beesCount = random.Next(5, options.BeehiveCapacity);
                var currentCount = 0;

                // добавим пчелу-матку
                if (currentCount < 1)
                {
                    var queen = new Bee();

                    queen.AddComponent(new Lifetime(TimeSpan.MaxValue));
                    queen.AddComponent(new QueenBee(beehive));

                    owner.AddBee(queen);
                    scene.AddObject(queen);

                    currentCount++;
                }

                // добавим пчёл-"охранников"
                for (var index = 0; index < 3 && currentCount < beesCount; index++, currentCount++)
                {
                    var guard = new Bee();

                    guard.AddComponent(new Lifetime(TimeSpan.FromMinutes(5.0d)));
                    guard.AddComponent(new GuardBee(beehive));

                    owner.AddBee(guard);
                    scene.AddObject(guard);
                }

                // добавим рабочих пчёл
                while (currentCount++ < beesCount)
                {
                    var bee = new Bee();

                    bee.AddComponent(new Lifetime(TimeSpan.FromMinutes(1.0d)));
                    bee.AddComponent(new WorkBee());

                    owner.AddBee(bee);
                    scene.AddObject(bee);
                }
            }
        }
    }
}