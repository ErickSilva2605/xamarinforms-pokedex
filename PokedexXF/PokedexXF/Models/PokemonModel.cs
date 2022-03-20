using LiteDB;
using Newtonsoft.Json;
using PokedexXF.Enums;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokedexXF.Models
{
    public class PokemonModel : ResourceBaseModel
    {
        [BsonId]
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_POKEMON;

        [JsonProperty("abilities")]
        public IEnumerable<PokemonAbilityModel> Abilities { get; set; }

        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        public double Meters { get; set; }

        public string Inches { get; set; }

        public string ImageUrl => string.Format(Constants.BASE_IMAGE_URL, Id);

        public bool IsBusy { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("species")]
        public PokemonSpeciesModel Species { get; set; }

        [JsonProperty("stats")]
        public IEnumerable<PokemonStatModel> Stats { get; set; }

        public int TotalStat { get; set; }

        [JsonProperty("types")]
        public IEnumerable<PokemonTypeModel> Types { get; set; }

        public TypeEnum TypeDefault
        {
            get
            {
                if (Enum.TryParse(Types.ToList()[0].Type.NameFirstCharUpper, out TypeEnum type))
                    return type;
                else
                    return TypeEnum.Undefined;
            }
        }

        public IEnumerable<PokemonTypeDefenseModel> TypeDefenses { get; set; }

        public IEnumerable<PokemonTypeDefenseModel> Weaknesses { get; set; }


        [JsonProperty("weight")]
        public int Weight { get; set; }

        public double Kilograms { get; set; }

        public double Pounds { get; set; }
    }
}
