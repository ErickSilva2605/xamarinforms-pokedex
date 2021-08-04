using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PokedexXF.Converters
{
    public class ConverterTypeToColorType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return null;

            TypeEnum type;
            if (!Enum.TryParse((string)value, out type))
                type = TypeEnum.Undefined;

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
