using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class ResultModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
