using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonVersionModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
