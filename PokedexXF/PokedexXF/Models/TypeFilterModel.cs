﻿using PokedexXF.Enums;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public class TypeFilterModel : ObservableObject
    {
        public TypeEnum Type { get; set; }

        private bool _selected;
        public bool Selected 
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
