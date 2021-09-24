using Newtonsoft.Json;
using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokedexXF.Models
{
    public class DoubleDamageFromModel
    {
        [JsonProperty("name")]
        private string _name;
        public string Name
        {
            get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name.ToLower());
            set => _name = value;
        }

        public TypeEnum Type
        {
            get
            {
                if (Enum.TryParse(Name, out TypeEnum type))
                    return type;
                else
                    return TypeEnum.Undefined;
            }
        }
    }
}
