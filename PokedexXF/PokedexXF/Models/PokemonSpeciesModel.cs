using Newtonsoft.Json;
using PokedexXF.Extensions;
using PokedexXF.Helpers;
using System.Collections.Generic;

namespace PokedexXF.Models
{
    public class PokemonSpeciesModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_SPECIES;

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("base_happiness")]
        public int BaseHappiness { get; set; }

        [JsonProperty("capture_rate")]
        public int CaptureRate { get; set; }

        public double CaptureProbability { get; set; }

        [JsonProperty("egg_groups")]
        public IEnumerable<EggGroupModel> EggGroups { get; set; }

        public string EggGroupsDescription { get; set; }

        [JsonProperty("evolution_chain")]
        public EvolutionChainModel EvolutionChain { get; set; }

        public string EvYield { get; set; }

        [JsonProperty("flavor_text_entries")]
        public IEnumerable<PokemonSpeciesFlavorTextsModel> FlavorTextEntries { get; set; }

        public string FlavorText { get; set; }

        [JsonProperty("genera")]
        public IEnumerable<GenusesModel> Genera { get; set; }

        public string GenusDescription { get; set; }

        [JsonProperty("gender_rate")]
        public int GenderRate { get; set; }

        public string GenderDescription { get; set; }

        [JsonProperty("hatch_counter")]
        public int HatchCounter { get; set; }

        public int MaxEggSteps { get; set; }

        public int MinEggSteps { get; set; }

        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifferences { get; set; }

        [JsonProperty("growth_rate")]
        public GrowthRateModel GrowthRate { get; set; }

        public string GrowthRateDescription { get; set; }

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonProperty("is_mythical")]
        public bool IsMythical { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("pokedex_numbers")]
        public IEnumerable<PokemonSpeciesDexEntryModel> PokedexNumbers { get; set; }

        public IEnumerable<LocationModel> Locations { get; set; }

        public IEnumerable<EvolutionModel> Evolutions { get; set; }

        public bool IsBusy { get; set; }

    }
}
