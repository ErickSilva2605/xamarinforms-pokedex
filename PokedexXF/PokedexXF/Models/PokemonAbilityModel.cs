using Newtonsoft.Json;
using PokedexXF.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonAbilityModel : ObservableObject
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("ability")]
        public AbilityModel Ability { get; set; }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
    }
}
