using PokedexXF.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterHeightToDescriptionValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int height = (int)value;

            double meters = height / Constants.METERS_CONVERTER_VALUE;
            double feet = Math.Round(meters / Constants.FEET_CONVERTER_VALUE, 3);
            double feetLeft = Math.Truncate(feet);
            double feetRight = feet - Math.Truncate(feet);
            double inch = feetRight / Constants.INCH_CONVERTER_VALUE;


            return $"{meters.ToString("N1", new CultureInfo("en-US"))}m ({feetLeft}'{Math.Truncate(inch).ToString().PadLeft(2, '0')}'')";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
