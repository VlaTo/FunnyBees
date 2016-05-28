using System.Threading.Tasks;
using LibraProgramming.FunnyBees.Models;

namespace LibraProgramming.FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionsChangedEventArgs
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="e"></param>
    public delegate void OptionsChangedEventHandler(IBeeApiarOptionsProvider provider, OptionsChangedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public interface IBeeApiarOptionsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        event OptionsChangedEventHandler OptionsChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<BeeApiarOptions> GetOptionsAsync();
    }
}