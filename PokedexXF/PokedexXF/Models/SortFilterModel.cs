using PokedexXF.Enums;
using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class SortFilterModel : ObservableObject
    {
        public SortEnum Sort { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
