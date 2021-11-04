﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokedexXF.Models
{
    public class StatModel
    {
        [JsonProperty("name")]
        private string _name;
        public string Name
        {
            get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name.ToLower().Replace('-', ' '));
            set => _name = value;
        }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}