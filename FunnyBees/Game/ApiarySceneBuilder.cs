using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Engine;
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
            var beehive = new Beehive();

            beehive.AddComponent<BeehiveOwnedBees>();

            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);
            var ownedBees = beehive.GetComponent<BeehiveOwnedBees>();

            scene.AddObject(beehive);

            for (var index = 0; index < options.MaximumNumberOfBees; index++)
            {
                var bee = new Bee();

                ownedBees.Bees.Add(bee);
                bee.AddComponent<Lifetime>();
                scene.AddObject(bee);
            }
        }
    }
}