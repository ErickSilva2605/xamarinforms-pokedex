using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class EvolutionDetailsModel
    {
        [JsonProperty("min_level")]
        public int MinLevel { get; set; }
    }
}
