using PokedexXF.Enums;
using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class WeightFilterModel : ObservableObject
    {
        public WeightEnum Weight { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
