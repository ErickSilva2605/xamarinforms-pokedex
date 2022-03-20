using Newtonsoft.Json;

namespace PokedexXF.Models
{
    public class PokemonStatModel 
    {
        [JsonProperty("stat")]
        public StatModel Stat { get; set; }

        [JsonProperty("base_stat")]
        public int BaseStat { get; set; }

        [JsonProperty("effort")]
        public int Effort { get; set; }

        public int MaxStat { get; set; }

        public int MinStat { get; set; }

        public double PercentageStat { get; set; }
    }
}
