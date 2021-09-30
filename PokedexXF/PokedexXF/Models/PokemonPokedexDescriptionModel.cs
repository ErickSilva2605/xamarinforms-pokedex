using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonPokedexDescriptionModel
    {
        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("language")]
        public PokemonLanguageModel Language { get; set; }
    }
}
