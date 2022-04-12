using Newtonsoft.Json;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class VersionGroupModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_VERSION_GROUP;

        public IEnumerable<VersionModel> Versions { get; set; }
    }
}
