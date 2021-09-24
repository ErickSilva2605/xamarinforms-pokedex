using Newtonsoft.Json;
using System.Threading;

namespace PokedexXF.Models
{
    public class PokemonAbilitiesModel
    {
        [JsonProperty("ability")]
        public PokemonAbilityModel Ability { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }
    }
}
