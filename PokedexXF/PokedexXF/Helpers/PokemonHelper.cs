using PokedexXF.Enums;
using PokedexXF.Extensions;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace PokedexXF.Helpers
{
    public static class PokemonHelper
    {
        public static string RefreshAndValidateNextPage(int offset, int pagesCount, string nextPage)
        {
            if (offset >= pagesCount || string.IsNullOrEmpty(nextPage))
                return null;

            if (nextPage.Contains($"?offset={offset}"))
                return nextPage;
            else
                return string.Format(Constants.BASE_URL_NEXT_PAGE, offset);
        }

        public static IEnumerable<PokemonModel> GetMockPokemonList(int offset, int amount)
        {
            List<PokemonModel> pokemonMockList = new List<PokemonModel>();

            for (int i = offset + 1; i <= amount; i++)
            {
                pokemonMockList.Add(new PokemonModel()
                {
                    Name = "XXXXXXXXXX",
                    Id = i,
                    Types = new ObservableRangeCollection<PokemonTypeModel>(GetMockPokemonTypes()),
                    IsBusy = true
                });
            }

            return pokemonMockList;
        }

        private static IEnumerable<PokemonTypeModel> GetMockPokemonTypes()
        {
            return new List<PokemonTypeModel>
            {
                new PokemonTypeModel { Slot = 1, Type = new TypeModel() { Name = "xxxxx" }, IsBusy = true },
                new PokemonTypeModel { Slot = 1, Type = new TypeModel() { Name = "xxxxx" }, IsBusy = true }
            };
        }

        public static PokemonSpeciesModel GetMockPokemonSpecies()
        {
            return new PokemonSpeciesModel()
            {
                BaseHappiness = 0,
                CaptureProbability = 0,
                CaptureRate = 0,
                EggGroups = new List<EggGroupModel>(),
                EggGroupsDescription = "XXXXXXXXXXXXXX",
                EvolutionChain = new EvolutionChainModel(),
                Evolutions = GetMockPokemonEvolutions(),
                EvYield = "XXXXXXXXXXXXXX",
                FlavorText = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                FlavorTextEntries = new List<PokemonSpeciesFlavorTextsModel>(),
                GenderDescription = "XXXXXXXXXXXXXX",
                GenderRate = 0,
                Genera = new List<GenusesModel>(),
                GenusDescription = "XXXXXXXXXXXXXX",
                GrowthRate = new GrowthRateModel(),
                GrowthRateDescription = "XXXXXXXXXXXXXX",
                HasGenderDifferences = false,
                HatchCounter = 0,
                Id = 0,
                IsBaby = false,
                IsBusy = true,
                IsLegendary = false,
                IsMythical = false,
                Locations = GetMockPokemonLocations(),
                MaxEggSteps = 0,
                MinEggSteps = 0,
                Name = "XXXXXXXXXXXXXX",
                Order = 0,
                PokedexNumbers = new List<PokemonSpeciesDexEntryModel>()
            };
        }

        public static IEnumerable<EvolutionModel> GetMockPokemonEvolutions()
        {
            return new List<EvolutionModel>
            {
                new EvolutionModel 
                {
                    Id = 0,
                    Name = "XXXXXXXXX",
                    Image = string.Empty,
                    EnvolvesToId = 0,
                    EnvolvesToImage = string.Empty,
                    EnvolvesToMinLevel = "XX",
                    EnvolvesToName = "XXXXXXXXX",
                    IsBaby = false,
                    HasEvolution = true,
                    IsBusy = true 
                },
                new EvolutionModel
                {
                    Id = 0,
                    Name = "XXXXXXXXX",
                    Image = string.Empty,
                    EnvolvesToId = 0,
                    EnvolvesToImage = string.Empty,
                    EnvolvesToMinLevel = "XX",
                    EnvolvesToName = "XXXXXXXXX",
                    IsBaby = false,
                    HasEvolution = true,
                    IsBusy = true
                }
            };
        }

        public static IEnumerable<LocationModel> GetMockPokemonLocations()
        {
            return new List<LocationModel>
            {
                new LocationModel { Description = "XXXXXXXXXXXXXX", EntryNumber = 100000, IsBusy = true },
                new LocationModel { Description = "XXXXXXXXXXXXXX", EntryNumber = 100000, IsBusy = true },
                new LocationModel { Description = "XXXXXXXXXXXXXX", EntryNumber = 100000, IsBusy = true },
                new LocationModel { Description = "XXXXXXXXXXXXXX", EntryNumber = 100000, IsBusy = true }
            };
        }

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

        public static IEnumerable<TypeFilterModel> GetFilterTypes()
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

        public static IEnumerable<WeaknessFilterModel> GetFilterWeaknesses()
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

        public static IEnumerable<HeightFilterModel> GetFilterHeights()
        {
            return new List<HeightFilterModel>
            {
                new HeightFilterModel{ Height = HeightEnum.Short },
                new HeightFilterModel{ Height = HeightEnum.Medium },
                new HeightFilterModel{ Height = HeightEnum.Tall }
            };
        }

        public static IEnumerable<WeightFilterModel> GetFilterWeights()
        {
            return new List<WeightFilterModel>
            {
                new WeightFilterModel{ Weight = WeightEnum.Light },
                new WeightFilterModel{ Weight = WeightEnum.Normal },
                new WeightFilterModel{ Weight = WeightEnum.Heavy }
            };
        }

        public static IEnumerable<SortFilterModel> GetFilterSorts()
        {
            return new List<SortFilterModel>()
            {
                new SortFilterModel { Sort = SortEnum.Ascending, Selected = true },
                new SortFilterModel { Sort = SortEnum.Descending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalAscending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalDescending }
            };
        }

        public static IEnumerable<GenerationFilterModel> GetFilterGenerations()
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

        public static IEnumerable<TypeRelationModel> GetAllTypeRelations(TypeRelationsModel typeRelations)
        {
            List<TypeRelationModel> allTypeRelations = new List<TypeRelationModel>();
            var types = Enum.GetValues(typeof(TypeEnum)).Cast<int>().ToList();

            foreach (var typeItem in types)
            {
                if ((TypeEnum)typeItem == TypeEnum.Undefined)
                    continue;

                TypeRelationModel typeRelation = new TypeRelationModel();
                typeRelation.Type = (TypeEnum)typeItem;

                if (typeRelations.DoubleDamageFrom.Any() && typeRelations.DoubleDamageFrom.Any(x => x.Type == typeRelation.Type))
                    typeRelation.Effect = EffectEnum.SuperEffective;
                else if (typeRelations.HalfDamageFrom.Any() && typeRelations.HalfDamageFrom.Any(x => x.Type == typeRelation.Type))
                    typeRelation.Effect = EffectEnum.NotVeryEffective;
                else if (typeRelations.NoDamageFrom.Any() && typeRelations.NoDamageFrom.Any(x => x.Type == typeRelation.Type))
                    typeRelation.Effect = EffectEnum.NoEffect;
                else
                    typeRelation.Effect = EffectEnum.Normal;

                allTypeRelations.Add(typeRelation);
            }

            return allTypeRelations;
        }

        public static IEnumerable<PokemonTypeDefenseModel> GetPokemonWeaknesses(IEnumerable<PokemonTypeDefenseModel> typeDefenses)
        {
            return typeDefenses.Where(w => w.Effect == EffectEnum.SuperEffective).ToList();
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

        public static string EggGroupsToDescription(IEnumerable<EggGroupModel> eggGroups)
        {
            return string.Join(", ", eggGroups.Select(s => s.NameFirstCharUpper));
        }

        public static string GetEvYield(IEnumerable<PokemonStatModel> stats)
        {
            string effort = stats.Where(w => w.Effort > 0).Select(x => x.Effort).FirstOrDefault().ToString();
            string name = stats.Where(w => w.Effort > 0).Select(x => x.Stat.Name).FirstOrDefault();

            if (string.IsNullOrEmpty(name))
                return string.Empty;

            string evYield = effort;

            foreach (var item in name.Replace('-', ' ').Split(' '))
                evYield += $" {item.FirstCharToUpper()}";

            return evYield.Trim();
        }

        public static string FlavorTextsToDescription(IEnumerable<PokemonSpeciesFlavorTextsModel> flavorTexts)
        {
            return flavorTexts
                .Where(w => w.Language.Name == "en" && w.Version.Name == "ruby")
                .Select(s => s.FlavorText)
                .FirstOrDefault()
                .Replace('\n', ' ')
                .Replace('\f', ' ');
        }

        public static string GeneraToDescription(IEnumerable<GenusesModel> genera)
        {
            return genera
                .Where(w => w.Language.Name == "en")
                .Select(s => s.Genus).FirstOrDefault();
        }

        public static string GenderRateToDescription(int rate)
        {
            if (rate < 0)
                return "Genderless";

            if (rate == 8)
                return $"♂ 0.0%, ♀ 100.0%";

            double female = (double)rate / 8 * 100;
            double male = 100 - female;

            return $"♂ {male.ToString("N1", new CultureInfo("en-US"))}%, ♀ {female.ToString("N1", new CultureInfo("en-US"))}%";
        }

        public static string GrowthRateToDescription(GrowthRateModel growthRate)
        {
            string growthRateDescription = string.Empty;

            foreach (var item in growthRate.Name.Replace('-', ' ').Split(' '))
                growthRateDescription += $"{item.FirstCharToUpper()} ";

            return growthRateDescription.Trim();
        }

        public static LocationModel GetLocation(int entryNumber, PokedexModel pokedex)
        {
            return new LocationModel
            {
                EntryNumber = entryNumber,
                Description = $"({pokedex.Descriptions.Where(w => w.Language.Name == "en").Select(s => s.Description).FirstOrDefault()})"
            };
        }

        public static double CalculateCatchRateProbability(IEnumerable<PokemonStatModel> stats, int captureRate)
        {
            int hp = stats.Where(w => w.Stat.Name.ToLower() == "hp").Select(s => s.BaseStat).FirstOrDefault();
            double alpha = (double)(((3 * hp) - (2 * hp)) * captureRate * 1 / (3 * hp)) * 1;
            return (alpha / 255);
        }

        public static int CalculateMaxEggSteps(int hatchCounter)
        {
            return hatchCounter * Constants.EGG_CYCLE_STEPS;
        }

        public static int CalculateMinEggSteps(int maxEggSteps)
        {
            return maxEggSteps - (Constants.EGG_CYCLE_STEPS - 1);
        }

        public static IEnumerable<PokemonStatModel> GetFullStats(IEnumerable<PokemonStatModel> stats)
        {
            foreach (var item in stats)
            {
                if (item.Stat.Name.ToLower() == "hp")
                {
                    item.Stat.StatDescription = item.Stat.NameFirstCharUpper;
                    item.MaxStat = (((2 * item.BaseStat) + Constants.IV_MAX + (Constants.EV_MAX / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + Constants.POKEMON_MAX_LEVEL + 10;
                    item.MinStat = (((2 * item.BaseStat) + Constants.IV_MIN + (Constants.EV_MIN / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + Constants.POKEMON_MAX_LEVEL + 10;
                }
                else
                {
                    if (item.Stat.Name.ToLower() == "special-attack" || item.Stat.Name.ToLower() == "special-defense")
                        item.Stat.StatDescription = item.Stat.Name.ToLower() == "special-attack" ? "Sp.Atk" : "Sp.Def";
                    else
                        item.Stat.StatDescription = item.Stat.NameFirstCharUpper;

                    item.MaxStat = (int)(((((2 * item.BaseStat) + Constants.IV_MAX + (Constants.EV_MAX / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + 5) * Constants.NATURE_MAX);
                    item.MinStat = (int)(((((2 * item.BaseStat) + Constants.IV_MIN + (Constants.EV_MIN / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + 5) * Constants.NATURE_MIN);
                }

                item.PercentageStat = (double)item.BaseStat / ((item.MaxStat + item.MinStat) / 2);
            }

            return stats;
        }

        public static int GetTotalStats(IEnumerable<PokemonStatModel> stats)
        {
            return stats.Sum(s => s.BaseStat);
        }

        public static double DecimetresToMeters(int decimetres)
        {
            return decimetres / Constants.METERS_CONVERTER_VALUE;
        }

        public static string MetersToInches(double meters)
        {
            var feet = UnitConverters.MetersToInternationalFeet(meters);
            double feetLeft = Math.Truncate(feet);
            double feetRight = feet - Math.Truncate(feet);
            double inch = feetRight / Constants.INCH_CONVERTER_VALUE;

            return $"({feetLeft}'{Math.Truncate(inch).ToString().PadLeft(2, '0')}'')";
        }

        public static double HectogramsToKilograms(int Hectograms)
        {
            return Hectograms / Constants.KILOGRAM_CONVERTER_VALUE;
        }

        public static double KilogramsToPounds(double kilograms)
        {
            return UnitConverters.KilogramsToPounds(kilograms);
        }
    }
}
