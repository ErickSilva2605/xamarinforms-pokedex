using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using PokedexXF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class HomeViewModel : BaseViewModel, IInitializeAsync
    {
        private readonly IRestService _service;
        private readonly LiteDbService<PokemonModel> _dbService;
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
            _dbService = new LiteDbService<PokemonModel>();
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
                {
                    // TODO - Mensagem dados offline
                    return;
                }

                await GetPaginationList(null);

                var dbPokemons = _dbService.FindAll();

                if (!dbPokemons.Any())
                {
                    Pokemons = new ObservableRangeCollection<PokemonModel>( await GetPokemons());

                    if (Pokemons.Any())
                        LiteDbHelper.UpdateDataBase(_dbService, Pokemons);
                }
                else
                {
                    await Task.Delay(4000);
                    Pokemons = new ObservableRangeCollection<PokemonModel>(dbPokemons.Take(20));
                }

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
                {
                    // TODO - Mensagem dados offline
                    return;
                }

                var mock = GetPokemonsMock();
                Pokemons.AddRange(mock);

                await GetPaginationList(Pagination.Next);

                var dbPokemons = _dbService.FindAll();

                if (!dbPokemons.Any() || dbPokemons.Count() <= Offset)
                {
                    var pokemonList = await GetPokemons();
                    Pokemons.RemoveRange(mock);
                    Pokemons.AddRange(pokemonList);

                    if (Pokemons.Any())
                        LiteDbHelper.UpdateDataBase(_dbService, Pokemons);
                }
                else
                {
                    await Task.Delay(2000);
                    Pokemons.RemoveRange(mock);
                    Pokemons.AddRange(dbPokemons.Skip(Offset).Take(Offset));
                }
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

        private async Task<List<PokemonModel>> GetPokemons()
        {
            try
            {
                List<PokemonModel> pokemonsList = new List<PokemonModel>();

                foreach (var item in Pagination.Results)
                {
                    var pokemon = await _service.GetPokemon(item.Url);

                    if (pokemon != null)
                        pokemonsList.Add(pokemon);
                }

                return pokemonsList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }

            return new List<PokemonModel>();
        }

        private IList<PokemonModel> GetPokemonsMock()
        {
            return PokemonMock.GetPokemonMock(Offset, Amount);
        }
    }
}
