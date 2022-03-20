using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class ResourceItemModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
