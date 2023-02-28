using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace PokedexXF.Converters
{
    public class ConverterGenerationToDescriptionGeneration : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GenerationEnum type = GenerationEnum.GenerationOne;

            if (!(value is GenerationEnum))
            {
                if (!(value is string))
                    return null;

                if (!Enum.TryParse((string)value, out type))
                    type = GenerationEnum.GenerationOne;
            }
            else
                type = (GenerationEnum)value;

            switch (type)
            {
                case GenerationEnum.GenerationOne:
                    return "Generation I";
                case GenerationEnum.GenerationTwo:
                    return "Generation II";
                case GenerationEnum.GenerationThree:
                    return "Generation III";
                case GenerationEnum.GenerationFour:
                    return "Generation VI";
                case GenerationEnum.GenerationFive:
                    return "Generation V";
                case GenerationEnum.GenerationSix:
                    return "Generation VI";
                case GenerationEnum.GenerationSeven:
                    return "Generation VII";
                case GenerationEnum.GenerationEight:
                    return "Generation VIII";
                default:
                    return "Generation I";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
