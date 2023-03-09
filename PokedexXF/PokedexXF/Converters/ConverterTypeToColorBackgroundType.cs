using PokedexXF.Enums;
using System.Globalization;
using PokedexXF.Extensions;

namespace PokedexXF.Converters
{
    public class ConverterTypeToColorBackgroundType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TypeEnum))
                return Application.Current.Resources.FindResource("ColorGray");

            var type = (TypeEnum)value;

            switch (type)
            {
                case TypeEnum.Bug:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeBug");
                case TypeEnum.Dark:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeDark");
                case TypeEnum.Dragon:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeDragon");
                case TypeEnum.Electric:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeElectric");
                case TypeEnum.Fairy:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeFairy");
                case TypeEnum.Fighting:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeFighting");
                case TypeEnum.Fire:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeFire");
                case TypeEnum.Flying:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeFlying");
                case TypeEnum.Ghost:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeGhost");
                case TypeEnum.Grass:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeGrass");
                case TypeEnum.Ground:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeGround");
                case TypeEnum.Ice:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeIce");
                case TypeEnum.Normal:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeNormal");
                case TypeEnum.Poison:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypePoison");
                case TypeEnum.Psychic:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypePsychic");
                case TypeEnum.Rock:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeRock");
                case TypeEnum.Steel:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeSteel");
                case TypeEnum.Water:
                    return Application.Current.Resources.FindResource("ColorBackgroundTypeWater");
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
