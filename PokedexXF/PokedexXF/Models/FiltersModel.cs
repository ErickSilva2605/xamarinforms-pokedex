using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class FiltersModel : ObservableObject
    {
        private ObservableRangeCollection<TypeFilterModel> _types;
        public ObservableRangeCollection<TypeFilterModel> Types 
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private ObservableRangeCollection<WeaknessFilterModel> _weaknesses;
        public ObservableRangeCollection<WeaknessFilterModel> Weaknesses 
        {
            get => _weaknesses; 
            set => SetProperty(ref _weaknesses, value);
        }

        private ObservableRangeCollection<HeightFilterModel> _heights;
        public ObservableRangeCollection<HeightFilterModel> Heights 
        {
            get => _heights; 
            set => SetProperty(ref _heights, value);
        }

        private ObservableRangeCollection<WeightFilterModel> _weights;
        public ObservableRangeCollection<WeightFilterModel> Weights 
        {
            get => _weights;
            set => SetProperty(ref _weights, value);
        }

        private ObservableRangeCollection<GenerationFilterModel> _generations;
        public ObservableRangeCollection<GenerationFilterModel> Generations 
        {
            get => _generations;
            set => SetProperty(ref _generations, value);
        }

        private ObservableRangeCollection<SortFilterModel> _orders;
        public ObservableRangeCollection<SortFilterModel> Orders 
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
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
