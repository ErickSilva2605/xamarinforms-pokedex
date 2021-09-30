using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonSpeciesInfoModel
    {
        [JsonProperty("base_happiness")]
        public int BaseHappiness { get; set; }

        [JsonProperty("capture_rate")]
        public int CaptureRate { get; set; }

        [JsonProperty("egg_groups")]
        public ObservableRangeCollection<PokemonEggGroupModel> EggGroups { get; set; }

        [JsonProperty("flavor_text_entries")]
        public ObservableRangeCollection<PokemonFlavorTextEntriesModel> FlavorTextEntries { get; set; }

        [JsonProperty("gender_rate")]
        public int GenderRate { get; set; }

        [JsonProperty("genera")]
        public ObservableRangeCollection<PokemonGeneraModel> Genera { get; set; }

        [JsonProperty("growth_rate")]
        public PokemonGrowthRateModel GrowthRate { get; set; }

        [JsonProperty("hatch_counter")]
        public int HatchCounter { get; set; }

        [JsonProperty("pokedex_numbers")]
        public ObservableRangeCollection<PokemonPokedexNumberModel> PokedexNumbers { get; set; }
    }
}
