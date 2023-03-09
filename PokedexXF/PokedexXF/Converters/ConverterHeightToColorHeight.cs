using PokedexXF.Enums;
using System.Globalization;
using PokedexXF.Extensions;

namespace PokedexXF.Converters
{
    public class ConverterHeightToColorHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HeightEnum type = HeightEnum.Undefined;

            if (!(value is HeightEnum))
            {
                if (!(value is string))
                    return Application.Current.Resources.FindResource("ColorGray");

                if (!Enum.TryParse((string)value, out type))
                    type = HeightEnum.Undefined;
            }
            else
                type = (HeightEnum)value;

            switch (type)
            {
                case HeightEnum.Short:
                    return Application.Current.Resources.FindResource("ColorHeightShort");
                case HeightEnum.Medium:
                    return Application.Current.Resources.FindResource("ColorHeightMedium");
                case HeightEnum.Tall:
                    return Application.Current.Resources.FindResource("ColorHeightTall");
                case HeightEnum.Undefined:
                    return Application.Current.Resources.FindResource("ColorGray");
                default:
                    return Application.Current.Resources.FindResource("ColorGray");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
