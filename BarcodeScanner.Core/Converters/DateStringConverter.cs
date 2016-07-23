using System;
using MvvmCross.Platform.Converters;

namespace BarcodeScanner.Core.Converters
{
    public class DateStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null || !(parameter is string))
            {
                return value.ToString();
            }

            return value.ToString(parameter as string);
        }
    }
}

