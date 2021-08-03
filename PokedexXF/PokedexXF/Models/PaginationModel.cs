using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PaginationModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public IEnumerable<ResultModel> Results { get; set; }
    }
}
