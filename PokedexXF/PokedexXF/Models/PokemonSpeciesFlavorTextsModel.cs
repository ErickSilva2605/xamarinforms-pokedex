using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class PokemonSpeciesFlavorTextsModel
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public LanguageModel Language { get; set; }

        [JsonProperty("version")]
        public VersionModel Version { get; set; }
    }
}
