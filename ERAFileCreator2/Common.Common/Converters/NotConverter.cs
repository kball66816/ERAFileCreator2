using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Common.Converters
{
    public class NotConverter : IValueConverter
    {
        /// <summary>
        /// WPF does not have a negative bool value, by using this converter this will allow us to evaluate !bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
