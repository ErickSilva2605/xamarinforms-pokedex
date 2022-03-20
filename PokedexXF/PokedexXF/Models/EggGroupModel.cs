using Newtonsoft.Json;
using PokedexXF.Extensions;
using PokedexXF.Helpers;

namespace PokedexXF.Models
{
    public class EggGroupModel : ResourceBaseModel
    {
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_EGG_GROUP;
    }
}
