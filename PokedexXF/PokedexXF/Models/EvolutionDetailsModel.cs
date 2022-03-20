using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class EvolutionDetailsModel
    {
        [JsonProperty("min_level")]
        public int? MinLevel { get; set; }
    }
}
