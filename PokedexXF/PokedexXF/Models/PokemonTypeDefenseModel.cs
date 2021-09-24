using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexXF.Models
{
    public class PokemonTypeDefenseModel
    {
        public string Multiplier { get; set; }

        public EffectEnum Effect { get; set; }

        public TypeEnum Type { get; set; }
    }
}
