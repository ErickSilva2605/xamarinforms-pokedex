using PokedexXF.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterTypeToColorBackgroundType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TypeEnum))
                return null;

            var type = (TypeEnum)value;

            switch (type)
            {
                case TypeEnum.Bug:
                    return Application.Current.Resources["ColorBackgroundTypeBug"];
                case TypeEnum.Dark:
                    return Application.Current.Resources["ColorBackgroundTypeDark"];
                case TypeEnum.Dragon:
                    return Application.Current.Resources["ColorBackgroundTypeDragon"];
                case TypeEnum.Electric:
                    return Application.Current.Resources["ColorBackgroundTypeElectric"];
                case TypeEnum.Fairy:
                    return Application.Current.Resources["ColorBackgroundTypeFairy"];
                case TypeEnum.Fighting:
                    return Application.Current.Resources["ColorBackgroundTypeFighting"];
                case TypeEnum.Fire:
                    return Application.Current.Resources["ColorBackgroundTypeFire"];
                case TypeEnum.Flying:
                    return Application.Current.Resources["ColorBackgroundTypeFlying"];
                case TypeEnum.Ghost:
                    return Application.Current.Resources["ColorBackgroundTypeGhost"];
                case TypeEnum.Grass:
                    return Application.Current.Resources["ColorBackgroundTypeGrass"];
                case TypeEnum.Ground:
                    return Application.Current.Resources["ColorBackgroundTypeGround"];
                case TypeEnum.Ice:
                    return Application.Current.Resources["ColorBackgroundTypeIce"];
                case TypeEnum.Normal:
                    return Application.Current.Resources["ColorBackgroundTypeNormal"];
                case TypeEnum.Poison:
                    return Application.Current.Resources["ColorBackgroundTypePoison"];
                case TypeEnum.Psychic:
                    return Application.Current.Resources["ColorBackgroundTypePsychic"];
                case TypeEnum.Rock:
                    return Application.Current.Resources["ColorBackgroundTypeRock"];
                case TypeEnum.Steel:
                    return Application.Current.Resources["ColorBackgroundTypeSteel"];
                case TypeEnum.Water:
                    return Application.Current.Resources["ColorBackgroundTypeWater"];
                case TypeEnum.Undefined:
                    return Color.Default;
                default:
                    return Color.Default;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
