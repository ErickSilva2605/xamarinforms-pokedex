using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class ChainModel
    {
        [JsonProperty("evolution_details")]
        public ObservableRangeCollection<EvolutionDetailsModel> EvolutionDetails  { get; set; }

        [JsonProperty("evolves_to")]
        public ObservableRangeCollection<ChainModel> EvolvesTo { get; set; }

        [JsonProperty("species")]
        public PokemonSpeciesModel Species { get; set; }

    }
}
