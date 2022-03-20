using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class PokemonAbilityModel 
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("ability")]
        public AbilityModel Ability { get; set; }
    }
}
