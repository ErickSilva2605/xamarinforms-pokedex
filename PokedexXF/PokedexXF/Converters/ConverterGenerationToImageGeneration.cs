using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace PokedexXF.Converters
{
    public class ConverterGenerationToImageGeneration : IValueConverter
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
                    return "generation_one";
                case GenerationEnum.GenerationTwo:
                    return "generation_two";
                case GenerationEnum.GenerationThree:
                    return "generation_three";
                case GenerationEnum.GenerationFour:
                    return "generation_four";
                case GenerationEnum.GenerationFive:
                    return "generation_five";
                case GenerationEnum.GenerationSix:
                    return "generation_six";
                case GenerationEnum.GenerationSeven:
                    return "generation_seven";
                case GenerationEnum.GenerationEight:
                    return "generation_eight";
                default:
                    return "generation_one";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
