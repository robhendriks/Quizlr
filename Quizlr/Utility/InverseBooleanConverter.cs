using System;
using System.Globalization;
using System.Windows.Data;

namespace Quizlr.Utility
{
    [ValueConversion(typeof (bool), typeof (bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new NotSupportedException();
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}