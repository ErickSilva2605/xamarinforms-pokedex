using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class FiltersModel : ObservableObject
    {
        public ObservableRangeCollection<TypeFilterModel> Types { get; set; }

        public ObservableRangeCollection<WeaknessFilterModel> Weaknesses { get; set; }

        public ObservableRangeCollection<HeightFilterModel> Heights { get; set; }

        public ObservableRangeCollection<WeightFilterModel> Weights { get; set; }

        private SortEnum _sort;
        public SortEnum Sort 
        {
            get => _sort;
            set => SetProperty(ref _sort, value);
        }

        private int _numberRangeMin;
        public int NumberRangeMin 
        {
            get => _numberRangeMin;
            set => SetProperty(ref _numberRangeMin, value);
        }

        private int _numberRangeMax;
        public int NumberRangeMax
        {
            get => _numberRangeMax;
            set => SetProperty(ref _numberRangeMax, value);
        }
    }
}
