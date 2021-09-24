using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonGeneraModel
    {
        [JsonProperty("genus")]
        public string Genus { get; set; }

        [JsonProperty("language")]
        public PokemonLanguageModel Language { get; set; }
    }
}
