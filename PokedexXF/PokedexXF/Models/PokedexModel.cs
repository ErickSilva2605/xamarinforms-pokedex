using Newtonsoft.Json;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class PokedexModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_POKEDEX;

        [JsonProperty("is_main_series")]
        public bool IsMainSeries { get; set; }

        [JsonProperty("descriptions")]
        public IEnumerable<DescriptionModel> Descriptions { get; set; }
    }
}
