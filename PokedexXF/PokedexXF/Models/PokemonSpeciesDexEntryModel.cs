using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class PokemonSpeciesDexEntryModel
    {
        [JsonProperty("entry_number")]
        public int EntryNumber { get; set; }

        [JsonProperty("pokedex")]
        public PokedexModel Pokedex { get; set; }

    }
}
