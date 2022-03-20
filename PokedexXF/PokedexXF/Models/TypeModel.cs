using Newtonsoft.Json;
using PokedexXF.Enums;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class TypeModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_TYPE;

        [JsonProperty("damage_relations")]
        public TypeRelationsModel DamageRelations { get; set; }

        public IEnumerable<TypeRelationModel> AllTypeRelations { get; set; }

        public TypeEnum Type
        {
            get
            {
                if (Enum.TryParse(NameFirstCharUpper, out TypeEnum type))
                    return type;
                else
                    return TypeEnum.Undefined;
            }
        }
    }
}
