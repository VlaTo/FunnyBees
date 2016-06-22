using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace FunnyBees.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ValueFormatConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var culture = String.IsNullOrEmpty(language)
                ? CultureInfo.InvariantCulture
                : new CultureInfo(language);
            return String.Format(culture, (string) parameter, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}