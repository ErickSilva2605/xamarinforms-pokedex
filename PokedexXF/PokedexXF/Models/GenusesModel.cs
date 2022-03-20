using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class GenusesModel
    {
        [JsonProperty("genus")]
        public string Genus { get; set; }

        [JsonProperty("language")]
        public LanguageModel Language { get; set; }
    }
}
