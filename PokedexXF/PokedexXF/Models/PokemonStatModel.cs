using Newtonsoft.Json;
using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonStatModel : ObservableObject
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

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
    }
}
