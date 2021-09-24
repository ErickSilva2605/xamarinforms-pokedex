using PokedexXF.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterWeightToDescriptionValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int weight = (int)value;

            double kilogram = weight / Constants.KILOGRAM_CONVERTER_VALUE;
            double lbs = kilogram * Constants.POUNDS_CONVERTER_VALUE;

            return $"{kilogram.ToString("N1", new CultureInfo("en-US"))}Kg ({lbs.ToString("N1", new CultureInfo("en-US"))} lbs)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
