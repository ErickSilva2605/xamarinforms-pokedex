using Newtonsoft.Json;
using PokedexXF.Helpers;

namespace PokedexXF.Models
{
    public class ResourceItemModel
    {
        public int Id { get => PokemonHelper.ExtractIdFromUrl(Url); }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
