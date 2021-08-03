using PokedexXF.Interfaces;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
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

        public ObservableRangeCollection<PokemonModel> Pokemons { get; private set; }
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

                foreach (var item in pagination.Results)
                {
                    var pokemon = await _service.GetPokemon(item.Url);

                    if (pokemon != null)
                        Pokemons.Add(pokemon);
                }

            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
