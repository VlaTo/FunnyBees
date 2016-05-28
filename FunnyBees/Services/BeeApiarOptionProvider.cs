using System;
using System.Threading.Tasks;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BeeApiarOptionProvider : IBeeApiarOptionsProvider
    {
        private readonly WeakEvent<OptionsChangedEventHandler> optionsChanged;

        /// <summary>
        /// 
        /// </summary>
        public event OptionsChangedEventHandler OptionsChanged
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
        public BeeApiarOptionProvider()
        {
            optionsChanged = new WeakEvent<OptionsChangedEventHandler>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<BeeApiarOptions> GetOptionsAsync()
        {
            return Task.FromResult(new BeeApiarOptions
            {
                Interval = TimeSpan.FromMilliseconds(300.0d),
                NumberOfBeehives = 4
            });
        }
    }
}