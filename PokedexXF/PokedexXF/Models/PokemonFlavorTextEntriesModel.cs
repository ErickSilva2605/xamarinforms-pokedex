using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonFlavorTextEntriesModel
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public PokemonLanguageModel Language { get; set; }

        [JsonProperty("version")]
        public PokemonVersionModel Version { get; set; }
    }
}
