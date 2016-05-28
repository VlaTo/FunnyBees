using Windows.ApplicationModel.Resources.Core;
using LibraProgramming.Windows.UI.Xaml.Localization;

namespace FunnyBees.Localization
{
    public sealed class ApplicationLocalizationManager : LocalizationManager, IApplicationLocalization
    {
        private const string ApplicationNameResourceKey = "ApplicationName";

        public static readonly ApplicationLocalizationManager Current = new ApplicationLocalizationManager();

        string IApplicationLocalization.ApplicationName => GetString(ApplicationNameResourceKey);

        private ApplicationLocalizationManager()
        {
            // Resources/LibraProgramming/Sample/UI/Xaml/TestEnum/Failed
            DefaultResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
        }
    }
}