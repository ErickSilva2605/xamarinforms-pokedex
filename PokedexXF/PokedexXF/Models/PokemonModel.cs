using LiteDB;
using Newtonsoft.Json;
using PokedexXF.Enums;
using PokedexXF.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonModel : ObservableObject
    {
        [JsonProperty("abilities")]
        public ObservableRangeCollection<PokemonAbilitiesModel> Abilities { get; set; }

        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        private int _baseHappiness;
        public int BaseHappiness
        {
            get => _baseHappiness;
            set => SetProperty(ref _baseHappiness, value);
        }

        private int _captureRate;
        public int CaptureRate
        {
            get => _captureRate;
            set => SetProperty(ref _captureRate, value);
        }

        private string _eggGroups;
        public string EggGroups
        {
            get => _eggGroups;
            set => SetProperty(ref _eggGroups, value);
        }

        public string EvYield 
        {
            get
            {
                if(Stats.Any())
                   return $"{Stats.Where(w => w.Effort > 0).Select(x => x.Effort).FirstOrDefault()} " +
                        $"{Stats.Where(w => w.Effort > 0).Select(x => x.Stat.Name).FirstOrDefault()}";

                return string.Empty;
            }
        }

        private string _flavorText;
        public string FlavorText
        {
            get => _flavorText;
            set => SetProperty(ref _flavorText, value);
        }

        private string _genderRate;
        public string GenderRate
        {
            get => _genderRate;
            set => SetProperty(ref _genderRate, value);
        }

        private string _genus;
        public string Genus
        {
            get => _genus;
            set => SetProperty(ref _genus, value);
        }

        private string _growthRate;
        public string GrowthRate
        {
            get => _growthRate;
            set => SetProperty(ref _growthRate, value);
        }

        private int _hatchCounter;
        public int HatchCounter
        {
            get => _hatchCounter;
            set => SetProperty(ref _hatchCounter, value);
        }

        [JsonProperty("height")]
        public int Height { get; set; }

        [BsonId]
        [JsonProperty("id")]
        public int Id { get; set; }

        public bool IsBusy { get; set; }

        public ObservableRangeCollection<PokemonLocationModel> Locations { get; set; }

        [JsonProperty("name")]
        private string _name;
        public string Name
        {
            get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name.ToLower());
            set => _name = value;
        }

        public string NameUpperCase
        {
            get => Name.ToUpper();
        }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("species")]
        public PokemonSpeciesModel Species { get; set; }

        [JsonProperty("sprites")]
        public PokemonSpritesModel Sprites { get; set; }

        [JsonProperty("stats")]
        public ObservableRangeCollection<PokemonStatModel> Stats { get; set; }

        [JsonProperty("types")]
        public ObservableRangeCollection<PokemonTypeModel> Types { get; set; }

        private ObservableRangeCollection<PokemonTypeDefenseModel> _typeDefenses = new ObservableRangeCollection<PokemonTypeDefenseModel>();
        public ObservableRangeCollection<PokemonTypeDefenseModel> TypeDefenses 
        {
            get => _typeDefenses;
            set => SetProperty(ref _typeDefenses, value);
        }


        private ObservableRangeCollection<PokemonTypeDefenseModel> _weaknesses = new ObservableRangeCollection<PokemonTypeDefenseModel>();
        public ObservableRangeCollection<PokemonTypeDefenseModel> Weaknesses
        {
            get => _weaknesses;
            set => SetProperty(ref _weaknesses, value);
        }

        public TypeEnum TypeDefault
        {
            get
            {
                if (Enum.TryParse(Types.ToList()[0].Type.Name, out TypeEnum type))
                    return type;
                else
                    return TypeEnum.Undefined;
            }
        }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
