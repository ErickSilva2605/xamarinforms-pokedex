using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class EvolutionChainModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("chain")]
        public ChainLinkModel Chain { get; set;}
    }
}
