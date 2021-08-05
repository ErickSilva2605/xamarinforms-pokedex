using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonTypeModel
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public TypeModel Type { get; set; }

        public bool IsBusy { get; set; }
    }
}
