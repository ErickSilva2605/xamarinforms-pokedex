using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class ResourceListModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public IEnumerable<ResourceItemModel> Results { get; set; }
    }
}
