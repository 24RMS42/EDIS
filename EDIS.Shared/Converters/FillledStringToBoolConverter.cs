using System;
using System.Globalization;
using Xamarin.Forms;

namespace EDIS.Shared.Converters
{
    public class FillledStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var x = System.Convert.ToString(value);
                return string.IsNullOrEmpty(x);
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}