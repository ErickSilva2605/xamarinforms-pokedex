using PokedexXF.Enums;

namespace PokedexXF.Models
{
    public class PokemonTypeDefenseModel
    {
        public string Multiplier { get; set; }

        public EffectEnum Effect { get; set; }

        public TypeEnum Type { get; set; }
    }
}
