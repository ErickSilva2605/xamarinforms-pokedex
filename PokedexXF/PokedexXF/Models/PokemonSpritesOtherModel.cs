using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonSpritesOtherModel
    {
        [JsonProperty("official-artwork")]
        public PokemonSpritesOfficialArtworkModel OfficialArtwork { get; set; }
    }
}
