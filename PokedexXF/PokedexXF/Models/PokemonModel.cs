using Newtonsoft.Json;
using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonModel
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        private string _name;
        public string Name 
        { 
            get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name.ToLower());
            set => _name = value;
        }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("sprites")]
        public PokemonSpritesModel Sprites { get; set; }

        [JsonProperty("types")]
        public ObservableRangeCollection<PokemonTypeModel> Types { get; set; }

        public TypeEnum TypeDefault 
        { 
            get
            {
                if (Enum.TryParse(Types.ToList()[0].Type.Name, out TypeEnum type))
                    return type;
                else
                    return TypeEnum.Undefined;
            }
        }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
