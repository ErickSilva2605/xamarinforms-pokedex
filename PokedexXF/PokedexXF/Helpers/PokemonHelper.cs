using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokedexXF.Helpers
{
    public static class PokemonHelper
    {
        public static double EffectToMultiplier(EffectEnum effect)
        {
            switch (effect)
            {
                case EffectEnum.NoEffect:
                    return 0;

                case EffectEnum.NotVeryEffective:
                    return 0.5;

                case EffectEnum.Normal:
                    return 1.0;

                case EffectEnum.SuperEffective:
                    return 2.0;

                default:
                    throw new ArgumentOutOfRangeException("effect");
            }
        }

        public static EffectEnum MultiplierToEffect(double multiplier)
        {
            switch (multiplier)
            {
                case 0:
                    return EffectEnum.NoEffect;

                case 0.25:
                case 0.5:
                    return EffectEnum.NotVeryEffective;

                case 1.0:
                    return EffectEnum.Normal;

                case 2.0:
                case 4.0:
                    return EffectEnum.SuperEffective;

                default:
                    throw new ArgumentOutOfRangeException("effect");
            }
        }

        public static string MultiplierToDescription(double multiplier)
        {
            switch (multiplier)
            {
                case 0:
                    return "0";

                case 0.25:
                    return "¼";

                case 0.5:
                    return "½";

                case 1.0:
                    return "";

                case 2.0:
                    return "2";

                case 4.0:
                    return "4";

                default:
                    throw new ArgumentOutOfRangeException("effect");
            }
        }

        public static string GenderRateToDescription(int rate)
        {
            if (rate < 0)
                return "Genderless";

            if(rate == 8)
                return $"♀ 0.0%, ♂ 100.0%";

            double female = (double)rate / 8 * 100;
            double male = 100 - female;

            return $"♂ {male.ToString("N1", new CultureInfo("en-US"))}%, ♀ {female.ToString("N1", new CultureInfo("en-US"))}%";
        }
    }
}
