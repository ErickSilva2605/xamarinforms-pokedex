using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonLanguageModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
