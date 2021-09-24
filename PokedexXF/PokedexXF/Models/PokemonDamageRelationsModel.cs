using Newtonsoft.Json;
using PokedexXF.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class PokemonDamageRelationsModel
    {
        public TypeEnum TypeDefense { get; set; }

        [JsonProperty("damage_relations")]
        public DamageRelationsModel DamageRelations { get; set; }

        public ObservableRangeCollection<TypeRelationModel> AllTypeRelations { get; set; }
    }
}
