using System;
using System.Globalization;
using Xamarin.Forms;

namespace EDIS.Shared.Converters
{
    public class BoolIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var x = System.Convert.ToInt32(value);
                if (x == 1)
                    return true;
                if (x == 0)
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = value as bool?;
            if (x != null)
            {
                return x.Value ? 1 : 0;
            }
            return 0;
        }
    }
}