using PokedexXF.Enums;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Helpers
{
    public static class PokemonHelper
    {
        public static FiltersModel GetFilters()
        {
            return new FiltersModel
            {
                Types = new ObservableRangeCollection<TypeFilterModel>(GetFilterTypes()),
                Weaknesses = new ObservableRangeCollection<WeaknessFilterModel>(GetFilterWeaknesses()),
                Heights = new ObservableRangeCollection<HeightFilterModel>(GetFilterHeights()),
                Weights = new ObservableRangeCollection<WeightFilterModel>(GetFilterWeights()),
                Orders = new ObservableRangeCollection<SortFilterModel>(GetFilterSorts()),
                Generations = new ObservableRangeCollection<GenerationFilterModel>(GetFilterGenerations()),
                NumberRangeMin = 1,
                NumberRangeMax = 898
            };
        }

        public static IList<TypeFilterModel> GetFilterTypes()
        {
            return new List<TypeFilterModel>()
            {
                new TypeFilterModel { Type = TypeEnum.Bug },
                new TypeFilterModel { Type = TypeEnum.Dark },
                new TypeFilterModel { Type = TypeEnum.Dragon },
                new TypeFilterModel { Type = TypeEnum.Electric },
                new TypeFilterModel { Type = TypeEnum.Fairy },
                new TypeFilterModel { Type = TypeEnum.Fighting },
                new TypeFilterModel { Type = TypeEnum.Fire },
                new TypeFilterModel { Type = TypeEnum.Flying },
                new TypeFilterModel { Type = TypeEnum.Ghost },
                new TypeFilterModel { Type = TypeEnum.Grass },
                new TypeFilterModel { Type = TypeEnum.Ground },
                new TypeFilterModel { Type = TypeEnum.Ice },
                new TypeFilterModel { Type = TypeEnum.Poison },
                new TypeFilterModel { Type = TypeEnum.Psychic },
                new TypeFilterModel { Type = TypeEnum.Rock },
                new TypeFilterModel { Type = TypeEnum.Steel },
                new TypeFilterModel { Type = TypeEnum.Water }
            };
        }

        public static IList<WeaknessFilterModel> GetFilterWeaknesses()
        {
            return new List<WeaknessFilterModel>()
            {
                new WeaknessFilterModel {Type = TypeEnum.Bug },
                new WeaknessFilterModel {Type = TypeEnum.Dark },
                new WeaknessFilterModel {Type = TypeEnum.Dragon },
                new WeaknessFilterModel {Type = TypeEnum.Electric },
                new WeaknessFilterModel {Type = TypeEnum.Fairy },
                new WeaknessFilterModel {Type = TypeEnum.Fighting },
                new WeaknessFilterModel {Type = TypeEnum.Fire },
                new WeaknessFilterModel {Type = TypeEnum.Flying },
                new WeaknessFilterModel {Type = TypeEnum.Ghost },
                new WeaknessFilterModel {Type = TypeEnum.Grass },
                new WeaknessFilterModel {Type = TypeEnum.Ground },
                new WeaknessFilterModel {Type = TypeEnum.Ice },
                new WeaknessFilterModel {Type = TypeEnum.Poison },
                new WeaknessFilterModel {Type = TypeEnum.Psychic },
                new WeaknessFilterModel {Type = TypeEnum.Rock },
                new WeaknessFilterModel {Type = TypeEnum.Steel },
                new WeaknessFilterModel {Type = TypeEnum.Water }
            };
        }

        public static IList<HeightFilterModel> GetFilterHeights()
        {
            return new List<HeightFilterModel>
            {
                new HeightFilterModel{ Height = HeightEnum.Short },
                new HeightFilterModel{ Height = HeightEnum.Medium },
                new HeightFilterModel{ Height = HeightEnum.Tall }
            };
        }

        public static IList<WeightFilterModel> GetFilterWeights()
        {
            return new List<WeightFilterModel>
            {
                new WeightFilterModel{ Weight = WeightEnum.Light },
                new WeightFilterModel{ Weight = WeightEnum.Normal },
                new WeightFilterModel{ Weight = WeightEnum.Heavy }
            };
        }

        public static IList<SortFilterModel> GetFilterSorts()
        {
            return new List<SortFilterModel>()
            {
                new SortFilterModel { Sort = SortEnum.Ascending, Selected = true },
                new SortFilterModel { Sort = SortEnum.Descending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalAscending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalDescending }
            };
        }

        public static IList<GenerationFilterModel> GetFilterGenerations()
        {
            return new List<GenerationFilterModel>()
            {
                new GenerationFilterModel { Generation = GenerationEnum.GenerationOne },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationTwo },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationThree },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationFour },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationFive },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationSix },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationSeven },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationEight }
            };
        }

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

            if (rate == 8)
                return $"♀ 0.0%, ♂ 100.0%";

            double female = (double)rate / 8 * 100;
            double male = 100 - female;

            return $"♂ {male.ToString("N1", new CultureInfo("en-US"))}%, ♀ {female.ToString("N1", new CultureInfo("en-US"))}%";
        }
    }
}
