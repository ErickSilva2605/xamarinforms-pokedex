using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class DamageRelationsModel
    {
        [JsonProperty("double_damage_from")]
        public ObservableRangeCollection<DoubleDamageFromModel> DoubleDamageFrom { get; set; }

        [JsonProperty("half_damage_from")]
        public ObservableRangeCollection<HalfDamageFromModel> HalfDamageFrom { get; set; }

        [JsonProperty("no_damage_from")]
        public ObservableRangeCollection<NoDamageFromModel> NoDamageFrom { get; set; }
    }
}
