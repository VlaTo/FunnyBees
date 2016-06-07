using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using FunnyBees.Models;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationOptions Options
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.EventArgs"/>.
        /// </summary>
        public OptionsChangedEventArgs(ApplicationOptions options)
        {
            Options = options;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationOptionsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        event TypedEventHandler<IApplicationOptionsProvider, OptionsChangedEventArgs> OptionsChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ApplicationOptions> GetOptionsAsync(CancellationToken token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SaveOptionsAsync(ApplicationOptions options, CancellationToken token);
    }
}