using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using PokedexXF.Extensions;

namespace PokedexXF.Converters
{
    public class ConverterWeightToColorWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeightEnum type = WeightEnum.Undefined;

            if (!(value is WeightEnum))
            {
                if (!(value is string))
                    return Application.Current.Resources.FindResource("ColorGray");

                if (!Enum.TryParse((string)value, out type))
                    type = WeightEnum.Undefined;
            }
            else
                type = (WeightEnum)value;

            switch (type)
            {
                case WeightEnum.Light:
                    return Application.Current.Resources.FindResource("ColorWeightLight");
                case WeightEnum.Normal:
                    return Application.Current.Resources.FindResource("ColorWeightNormal");
                case WeightEnum.Heavy:
                    return Application.Current.Resources.FindResource("ColorWeightHeavy");
                case WeightEnum.Undefined:
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
