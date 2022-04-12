using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class NameModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public LanguageModel Language { get; set; }
    }
}
