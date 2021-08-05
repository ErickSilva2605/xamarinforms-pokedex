using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class HomeViewModel : BaseViewModel,  IInitializeAsync
    {
        private readonly IRestService _service;
        private ObservableRangeCollection<PokemonModel> _pokemons;

        public ObservableRangeCollection<PokemonModel> Pokemons 
        {
            get => _pokemons;
            set => SetProperty(ref _pokemons, value);
        }
        public HomeViewModel(INavigation navigation, IRestService restService) : base(navigation)
        {
            _service = restService;

            Pokemons = new ObservableRangeCollection<PokemonModel>();
        }

        public async Task InitializeAsync()
        {
            await GetPokemons();
        }

        private async Task GetPokemons()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (!InternetConnectivity())
                    return;

                var pagination = await _service.GetPokemons(null);

                if (pagination == null)
                    return;

                Pokemons.AddRange(PokemonMock.GetPokemonMock(20));
                List<PokemonModel> pokemonsList = new List<PokemonModel>();

                foreach (var item in pagination.Results)
                {
                    var pokemon = await _service.GetPokemon(item.Url);

                    if (pokemon != null)
                        pokemonsList.Add(pokemon);
                }

                Pokemons = new ObservableRangeCollection<PokemonModel>(pokemonsList);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
