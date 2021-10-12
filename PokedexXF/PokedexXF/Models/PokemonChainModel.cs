using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class PokemonChainModel
    {
        [JsonProperty("chain")]
        public ChainModel Chain { get; set; }
    }
}
