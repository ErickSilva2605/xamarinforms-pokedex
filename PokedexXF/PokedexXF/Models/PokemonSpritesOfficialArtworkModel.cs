using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonSpritesOfficialArtworkModel
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }
}
