using Newtonsoft.Json;
using System.Globalization;

namespace PokedexXF.Models
{
    public class PokemonEggGroupModel
    {
        [JsonProperty("name")]
        private string _name;
        public string Name
        {
            get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name.ToLower());
            set => _name = value;
        }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
