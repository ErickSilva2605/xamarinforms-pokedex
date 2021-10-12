using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonEvolutionChainModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
