using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterTypeToImageType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeEnum type = TypeEnum.Undefined;

            if (!(value is TypeEnum))
            {
                if (!(value is string))
                    return null;

                if (!Enum.TryParse((string)value, out type))
                    type = TypeEnum.Undefined;
            }
            else
                type = (TypeEnum)value;

            switch (type)
            {
                case TypeEnum.Bug:
                    return "type_bug";
                case TypeEnum.Dark:
                    return "type_dark";
                case TypeEnum.Dragon:
                    return "type_dragon";
                case TypeEnum.Electric:
                    return "type_electric";
                case TypeEnum.Fairy:
                    return "type_fairy";
                case TypeEnum.Fighting:
                    return "type_fighting";
                case TypeEnum.Fire:
                    return "type_fire";
                case TypeEnum.Flying:
                    return "type_flying";
                case TypeEnum.Ghost:
                    return "type_ghost";
                case TypeEnum.Grass:
                    return "type_grass";
                case TypeEnum.Ground:
                    return "type_Ground";
                case TypeEnum.Ice:
                    return "type_ice";
                case TypeEnum.Normal:
                    return "type_normal";
                case TypeEnum.Poison:
                    return "type_poison";
                case TypeEnum.Psychic:
                    return "type_psychic";
                case TypeEnum.Rock:
                    return "type_rock";
                case TypeEnum.Steel:
                    return "type_steel";
                case TypeEnum.Water:
                    return "type_water";
                case TypeEnum.Undefined:
                    return "";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
