using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonSpeciesInfoModel
    {
        [JsonProperty("flavor_text_entries")]
        public ObservableRangeCollection<PokemonFlavorTextEntriesModel> FlavorTextEntries { get; set; }

        [JsonProperty("genera")]
        public ObservableRangeCollection<PokemonGeneraModel> Genera { get; set; }
    }
}
