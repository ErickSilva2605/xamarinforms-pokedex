using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using PokedexXF.Extensions;

namespace PokedexXF.Converters
{
    public class ConverterTypeToColorType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeEnum type = TypeEnum.Undefined;

            if (!(value is TypeEnum))
            {
                if (!(value is string))
                    return Application.Current.Resources.FindResource("ColorGray");

                if (!Enum.TryParse((string)value, out type))
                    type = TypeEnum.Undefined;
            }
            else
                type = (TypeEnum)value;

            switch (type)
            {
                case TypeEnum.Bug:
                    return Application.Current.Resources.FindResource("ColorTypeBug");
                case TypeEnum.Dark:
                    return Application.Current.Resources.FindResource("ColorTypeDark");
                case TypeEnum.Dragon:
                    return Application.Current.Resources.FindResource("ColorTypeDragon");
                case TypeEnum.Electric:
                    return Application.Current.Resources.FindResource("ColorTypeElectric");
                case TypeEnum.Fairy:
                    return Application.Current.Resources.FindResource("ColorTypeFairy");
                case TypeEnum.Fighting:
                    return Application.Current.Resources.FindResource("ColorTypeFighting");
                case TypeEnum.Fire:
                    return Application.Current.Resources.FindResource("ColorTypeFire");
                case TypeEnum.Flying:
                    return Application.Current.Resources.FindResource("ColorTypeFlying");
                case TypeEnum.Ghost:
                    return Application.Current.Resources.FindResource("ColorTypeGhost");
                case TypeEnum.Grass:
                    return Application.Current.Resources.FindResource("ColorTypeGrass");
                case TypeEnum.Ground:
                    return Application.Current.Resources.FindResource("ColorTypeGround");
                case TypeEnum.Ice:
                    return Application.Current.Resources.FindResource("ColorTypeIce");
                case TypeEnum.Normal:
                    return Application.Current.Resources.FindResource("ColorTypeNormal");
                case TypeEnum.Poison:
                    return Application.Current.Resources.FindResource("ColorTypePoison");
                case TypeEnum.Psychic:
                    return Application.Current.Resources.FindResource("ColorTypePsychic");
                case TypeEnum.Rock:
                    return Application.Current.Resources.FindResource("ColorTypeRock");
                case TypeEnum.Steel:
                    return Application.Current.Resources.FindResource("ColorTypeSteel");
                case TypeEnum.Water:
                    return Application.Current.Resources.FindResource("ColorTypeWater");
                case TypeEnum.Undefined:
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
