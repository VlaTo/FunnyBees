using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace FunnyBees.Core
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var culture = String.IsNullOrEmpty(language) ? CultureInfo.InvariantCulture : new CultureInfo(language);
            return String.Format(culture, (string) parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}