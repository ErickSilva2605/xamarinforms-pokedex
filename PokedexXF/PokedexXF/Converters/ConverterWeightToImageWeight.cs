using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterWeightToImageWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeightEnum type = WeightEnum.Undefined;

            if (!(value is WeightEnum))
            {
                if (!(value is string))
                    return null;

                if (!Enum.TryParse((string)value, out type))
                    type = WeightEnum.Undefined;
            }
            else
                type = (WeightEnum)value;

            switch (type)
            {
                case WeightEnum.Light:
                    return "weight_light";
                case WeightEnum.Normal:
                    return "weight_normal";
                case WeightEnum.Heavy:
                    return "weight_heavy";
                case WeightEnum.Undefined:
                    return "weight_light";
                default:
                    return "weight_light";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
