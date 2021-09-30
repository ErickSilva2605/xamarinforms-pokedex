using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonPokedexNumberModel
    {
        [JsonProperty("entry_number")]
        public int EntryNumber { get; set; }

        [JsonProperty("pokedex")]
        public PokemonPokedexModel Pokedex { get; set; }

        [JsonProperty("descriptions")]
        public ObservableRangeCollection<PokemonPokedexDescriptionModel> Descriptions { get; set; }
    }
}
