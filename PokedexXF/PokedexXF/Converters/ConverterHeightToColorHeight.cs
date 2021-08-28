using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

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
                    return Application.Current.Resources["ColorGray"];

                if (!Enum.TryParse((string)value, out type))
                    type = HeightEnum.Undefined;
            }
            else
                type = (HeightEnum)value;

            switch (type)
            {
                case HeightEnum.Short:
                    return Application.Current.Resources["ColorHeightShort"];
                case HeightEnum.Medium:
                    return Application.Current.Resources["ColorHeightMedium"];
                case HeightEnum.Tall:
                    return Application.Current.Resources["ColorHeightTall"];
                case HeightEnum.Undefined:
                    return Application.Current.Resources["ColorGray"];
                default:
                    return Application.Current.Resources["ColorGray"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
