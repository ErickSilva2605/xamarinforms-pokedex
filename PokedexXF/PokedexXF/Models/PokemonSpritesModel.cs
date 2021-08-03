using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonSpritesModel
    {
        [JsonProperty("other")]
        public PokemonSpritesOtherModel Other { get; set; }
    }
}
