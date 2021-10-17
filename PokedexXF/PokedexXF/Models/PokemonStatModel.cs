using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonStatModel : ObservableObject
    {
        [JsonProperty("base_stat")]
        public int BaseStat { get; set; }

        [JsonProperty("effort")]
        public int Effort { get; set; }

        [JsonProperty("stat")]
        public StatModel Stat { get; set; }

        private int _maxStat;
        public int MaxStat 
        {
            get => _maxStat;
            set => SetProperty(ref _maxStat, value);
        }

        private int _minStat;
        public int MinStat 
        {
            get => _minStat;
            set => SetProperty(ref _minStat, value);
        }

        private double _percentageStat;
        public double PercentageStat 
        {
            get => _percentageStat;
            set => SetProperty(ref _percentageStat, value);
        }
    }
}
