using PokedexXF.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterHeightToImageHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HeightEnum type = HeightEnum.Undefined;

            if (!(value is HeightEnum))
            {
                if (!(value is string))
                    return null;

                if (!Enum.TryParse((string)value, out type))
                    type = HeightEnum.Undefined;
            }
            else
                type = (HeightEnum)value;

            switch (type)
            {
                case HeightEnum.Short:
                    return "height_short";
                case HeightEnum.Medium:
                    return "height_medium";
                case HeightEnum.Tall:
                    return "height_tall";
                case HeightEnum.Undefined:
                    return "height_short";
                default:
                    return "height_short";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
