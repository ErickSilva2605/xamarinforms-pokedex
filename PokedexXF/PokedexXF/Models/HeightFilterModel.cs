using PokedexXF.Enums;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class HeightFilterModel : ObservableObject
    {
        public HeightEnum Height { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
