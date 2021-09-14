using PokedexXF.Interfaces;
using PokedexXF.Models;
using PokedexXF.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private readonly IRestService _service;
        private readonly LiteDbService<PokemonModel> _dbService;
        private PokemonModel _pokemon;

        public Task Initialization { get; }

        public PokemonModel Pokemon
        {
            get => _pokemon;
            set => SetProperty(ref _pokemon, value);
        }

        public DetailViewModel(INavigation navigation, IRestService restService, PokemonModel pokemon) : base(navigation)
        {
            _service = restService;
            _dbService = new LiteDbService<PokemonModel>();
            Pokemon = pokemon;

            //Initialization = InitializeAsync();
        }

        //private async Task InitializeAsync()
        //{
            
        //}
    }
}
