using PokedexXF.Enums;
using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class GenerationFilterModel : ObservableObject
    {
        public GenerationEnum Generation { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
