using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using FunnyBees.Core;
using FunnyBees.Models;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ApplicationOptionProvider : IApplicationOptionsProvider
    {
        private readonly TypedWeakEventHandler<IApplicationOptionsProvider, OptionsChangedEventArgs> optionsChanged;
        private readonly RuntimeDataCache<ApplicationOptions> cache; 

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<IApplicationOptionsProvider, OptionsChangedEventArgs> OptionsChanged
        {
            add
            {
                optionsChanged.AddHandler(value);
            }
            remove
            {
                optionsChanged.RemoveHandler(value);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ApplicationOptionProvider()
        {
            optionsChanged = new TypedWeakEventHandler<IApplicationOptionsProvider, OptionsChangedEventArgs>();
            cache = new RuntimeDataCache<ApplicationOptions>(TimeSpan.FromMilliseconds(100.0d));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<ApplicationOptions> GetOptionsAsync(CancellationToken token)
        {
            if (null == token)
            {
                throw new ArgumentNullException(nameof(token));
            }

            ApplicationOptions options;

            if (false == cache.TryGetValue(out options))
            {
                options = new ApplicationOptions
                {
                    Interval = TimeSpan.FromMilliseconds(200.0d),
                    NumberOfBeehives = 2,
                    MinimumNumberOfBees = 25,
                    MaximumNumberOfBees = 35
                };

                cache.Set(options);
            }

            return Task.FromResult(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task SaveOptionsAsync(ApplicationOptions options, CancellationToken token)
        {
            if (null == options)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (null == token)
            {
                throw new ArgumentNullException(nameof(token));
            }

            cache.Set(options);

            optionsChanged.Invoke(this, new OptionsChangedEventArgs(options));

            return Task.CompletedTask;
        }
    }
}