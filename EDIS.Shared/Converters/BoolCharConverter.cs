using System;
using System.Globalization;
using Xamarin.Forms;

namespace EDIS.Shared.Converters
{
    public class BoolCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var x = System.Convert.ToString(value);
                return x == "Y" || x == "y";
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = value as bool?;
            if (x != null)
            {
                return x.Value ? 'Y' : 'N';
            }
            return 'N';
        }
    }
}