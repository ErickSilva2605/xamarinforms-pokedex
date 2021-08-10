using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class HomeViewModel : BaseViewModel, IInitializeAsync
    {
        private readonly IRestService _service;
        private ObservableRangeCollection<PokemonModel> _pokemons;
        private int _itemTreshold;

        public Task Initialization { get; }
        public int Offset { get; set; }

        public int Amount
        {
            get => Offset + 20;
        }

        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public PaginationModel Pagination { get; private set; }

        public ObservableRangeCollection<PokemonModel> Pokemons
        {
            get => _pokemons;
            set => SetProperty(ref _pokemons, value);
        }

        public ICommand LoadMorePokemonsCommand { get; set; }

        public HomeViewModel(INavigation navigation, IRestService restService) : base(navigation)
        {
            ItemTreshold = 1;
            _service = restService;

            Pagination = new PaginationModel();
            Pokemons = new ObservableRangeCollection<PokemonModel>();
            LoadMorePokemonsCommand = new Command(async () => await LoadMorePokemons());
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Pokemons.AddRange(GetPokemonsMock());
            await LoadPokemons();
        }

        private async Task GetPaginationList(string url)
        {
            try
            {
                if (!InternetConnectivity())
                    return;

                var pagination = await _service.GetPokemons(url);

                if (pagination == null)
                    return;

                Pagination = pagination;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task LoadPokemons()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (!InternetConnectivity())
                    return;

                await GetPaginationList(null);

                List<PokemonModel> pokemonsList = new List<PokemonModel>();

                foreach (var item in Pagination.Results)
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

        private async Task LoadMorePokemons()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Offset += 20;

                if (string.IsNullOrEmpty(Pagination.Next))
                {
                    ItemTreshold = -1;
                    return;
                }

                if (!InternetConnectivity())
                    return;

                var mock = GetPokemonsMock();
                Pokemons.AddRange(mock);

                await GetPaginationList(Pagination.Next);

                List<PokemonModel> pokemonsList = new List<PokemonModel>();

                foreach (var item in Pagination.Results)
                {
                    var pokemon = await _service.GetPokemon(item.Url);

                    if (pokemon != null)
                        pokemonsList.Add(pokemon);
                }

                Pokemons.RemoveRange(mock);
                Pokemons.AddRange(pokemonsList);
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

        private IList<PokemonModel> GetPokemonsMock()
        {
            return PokemonMock.GetPokemonMock(Offset, Amount);
        }
    }
}
