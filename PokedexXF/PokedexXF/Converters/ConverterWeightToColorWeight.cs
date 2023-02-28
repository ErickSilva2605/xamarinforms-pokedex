using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

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
                    return Application.Current.Resources["ColorGray"];

                if (!Enum.TryParse((string)value, out type))
                    type = WeightEnum.Undefined;
            }
            else
                type = (WeightEnum)value;

            switch (type)
            {
                case WeightEnum.Light:
                    return Application.Current.Resources["ColorWeightLight"];
                case WeightEnum.Normal:
                    return Application.Current.Resources["ColorWeightNormal"];
                case WeightEnum.Heavy:
                    return Application.Current.Resources["ColorWeightHeavy"];
                case WeightEnum.Undefined:
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
