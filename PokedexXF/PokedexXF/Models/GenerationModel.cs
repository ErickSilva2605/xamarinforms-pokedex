using Newtonsoft.Json;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class GenerationModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_GENERATION;

        [JsonProperty("pokemon_species")]
        public IEnumerable<PokemonSpeciesModel> Species { get; set; }
    }
}
