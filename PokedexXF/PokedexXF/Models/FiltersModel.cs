using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class FiltersModel : ObservableObject
    {
        private IEnumerable<TypeFilterModel> _types;
        public IEnumerable<TypeFilterModel> Types 
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private IEnumerable<WeaknessFilterModel> _weaknesses;
        public IEnumerable<WeaknessFilterModel> Weaknesses 
        {
            get => _weaknesses; 
            set => SetProperty(ref _weaknesses, value);
        }

        private IEnumerable<HeightFilterModel> _heights;
        public IEnumerable<HeightFilterModel> Heights 
        {
            get => _heights; 
            set => SetProperty(ref _heights, value);
        }

        private IEnumerable<WeightFilterModel> _weights;
        public IEnumerable<WeightFilterModel> Weights 
        {
            get => _weights;
            set => SetProperty(ref _weights, value);
        }

        private IEnumerable<GenerationFilterModel> _generations;
        public IEnumerable<GenerationFilterModel> Generations 
        {
            get => _generations;
            set => SetProperty(ref _generations, value);
        }

        private IEnumerable<SortFilterModel> _orders;
        public IEnumerable<SortFilterModel> Orders 
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
