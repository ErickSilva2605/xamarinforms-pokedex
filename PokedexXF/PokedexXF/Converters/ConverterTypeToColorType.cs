using PokedexXF.Enums;
using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

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
                    return Application.Current.Resources["ColorGray"];

                if (!Enum.TryParse((string)value, out type))
                    type = TypeEnum.Undefined;
            }
            else
                type = (TypeEnum)value;

            switch (type)
            {
                case TypeEnum.Bug:
                    return Application.Current.Resources["ColorTypeBug"];
                case TypeEnum.Dark:
                    return Application.Current.Resources["ColorTypeDark"];
                case TypeEnum.Dragon:
                    return Application.Current.Resources["ColorTypeDragon"];
                case TypeEnum.Electric:
                    return Application.Current.Resources["ColorTypeElectric"];
                case TypeEnum.Fairy:
                    return Application.Current.Resources["ColorTypeFairy"];
                case TypeEnum.Fighting:
                    return Application.Current.Resources["ColorTypeFighting"];
                case TypeEnum.Fire:
                    return Application.Current.Resources["ColorTypeFire"];
                case TypeEnum.Flying:
                    return Application.Current.Resources["ColorTypeFlying"];
                case TypeEnum.Ghost:
                    return Application.Current.Resources["ColorTypeGhost"];
                case TypeEnum.Grass:
                    return Application.Current.Resources["ColorTypeGrass"];
                case TypeEnum.Ground:
                    return Application.Current.Resources["ColorTypeGround"];
                case TypeEnum.Ice:
                    return Application.Current.Resources["ColorTypeIce"];
                case TypeEnum.Normal:
                    return Application.Current.Resources["ColorTypeNormal"];
                case TypeEnum.Poison:
                    return Application.Current.Resources["ColorTypePoison"];
                case TypeEnum.Psychic:
                    return Application.Current.Resources["ColorTypePsychic"];
                case TypeEnum.Rock:
                    return Application.Current.Resources["ColorTypeRock"];
                case TypeEnum.Steel:
                    return Application.Current.Resources["ColorTypeSteel"];
                case TypeEnum.Water:
                    return Application.Current.Resources["ColorTypeWater"];
                case TypeEnum.Undefined:
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
