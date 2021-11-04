using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using PokedexXF.Services;
using PokedexXF.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PokedexXF.ViewModels
{
    public class HomeViewModel : BaseViewModel, IInitializeAsync
    {
        private readonly IRestService _service;
        private readonly LiteDbService<PokemonModel> _dbService;
        private ObservableRangeCollection<PokemonModel> _pokemons;
        private FiltersModel _filters;
        private int _itemTreshold;
        private bool _drawerIsOpen;
        private double[] _lockStates = new double[] {};
        private bool _filterGenerationVisible;
        private bool _filterSortVisible;
        private bool _filtersVisible;

        public Task Initialization { get; }

        public bool DrawerIsOpen
        {
            get { return _drawerIsOpen; }
            set { SetProperty(ref _drawerIsOpen, value); }
        }

        public double[] LockStates
        {
            get => _lockStates;
            set => SetProperty(ref _lockStates, value);
        }

        public bool FilterGenerationVisible 
        {
            get => _filterGenerationVisible;
            set => SetProperty(ref _filterGenerationVisible, value);
        }

        public bool FilterSortVisible
        {
            get => _filterSortVisible;
            set => SetProperty(ref _filterSortVisible, value);
        }

        public bool FiltersVisible
        {
            get => _filtersVisible;
            set => SetProperty(ref _filtersVisible, value);
        }

        public int Offset 
        { 
            get; 
            set; 
        }

        public int Amount
        {
            get => Offset + 20;
        }

        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public FiltersModel Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        public PaginationModel Pagination 
        { 
            get; 
            private set; 
        }

        public ObservableRangeCollection<PokemonModel> Pokemons
        {
            get => _pokemons;
            set => SetProperty(ref _pokemons, value);
        }

        public ICommand LoadMorePokemonsCommand { get; set; }
        public ICommand OpenModalFiltersCommand { get; set; }
        public ICommand OpenModalSortCommand { get; set; }
        public ICommand OpenModalGenerationCommand { get; set; }
        public ICommand CloseModalCommand { get; set; }
        public ICommand NavigateToDetailCommand { get; set; }
        public ICommand SelectGenerationCommand { get; set; }
        public ICommand SelectSortCommand { get; set; }
        public ICommand SelectFilterTypeCommand { get; set; }
        public ICommand SelectFilterWeaknessCommand { get; set; }
        public ICommand SelectFilterHeightCommand { get; set; }
        public ICommand SelectFilterWeightCommand { get; set; }
        public ICommand ResetFiltersCommand { get; set; }
        public ICommand ApplyFiltersCommand { get; set; }

        public HomeViewModel(INavigation navigation, IRestService restService) : base(navigation)
        {
            ItemTreshold = 1;
            DrawerIsOpen = false;
            _service = restService;
            _dbService = new LiteDbService<PokemonModel>();
            Pagination = new PaginationModel();
            Pokemons = new ObservableRangeCollection<PokemonModel>();
            LoadMorePokemonsCommand = new Command(async () => await LoadMorePokemons());
            NavigateToDetailCommand = new Command<PokemonModel>(async (item) => await ExecuteNavigateToDetailCommand(item));
            OpenModalGenerationCommand = new Command(async () => await ExecuteOpenModalGenerationCommand());
            OpenModalSortCommand = new Command(async () => await ExecuteOpenModalSortCommand());
            OpenModalFiltersCommand = new Command(async () => await ExecuteOpenModalFiltersCommand());
            CloseModalCommand = new Command(async () => await ExecuteCloseModalCommand());
            SelectGenerationCommand = new Command<GenerationFilterModel>(async (generation) => await ExecuteSelectGenerationCommand(generation));
            SelectSortCommand = new Command<SortFilterModel>(async (sort) => await ExecuteSelectSortCommand(sort));
            SelectFilterTypeCommand = new Command<TypeFilterModel>((type) => ExecuteSelectFilterTypeCommand(type));
            SelectFilterWeaknessCommand = new Command<WeaknessFilterModel>((type) => ExecuteSelectFilterWeaknessCommand(type));
            SelectFilterHeightCommand = new Command<HeightFilterModel>((type) => ExecuteSelectFilterHeightCommand(type));
            SelectFilterWeightCommand = new Command<WeightFilterModel>((type) => ExecuteSelectFilterWeightCommand(type));
            ResetFiltersCommand = new Command(async () => await ExecuteResetFiltersCommand());
            ApplyFiltersCommand = new Command(async () => await ExecuteApplyFiltersCommand());
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Filters = PokemonHelper.GetFilters();
            Pokemons.AddRange(GetPokemonsMock());
            await LoadPokemons();
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

        private async Task ExecuteNavigateToDetailCommand(PokemonModel pokemon)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Navigation.PushAsync(new DetailPage(pokemon));
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

        private async Task ExecuteOpenModalGenerationCommand()
        {
            if (IsBusy)
                return;

            try
            {
                await Task.Run(() => 
                {
                    IsBusy = true;
                    LockStates = new double[] { 0, 0.35, 0.82 };
                    FilterGenerationVisible = true;
                    FilterSortVisible = false;
                    FiltersVisible = false;
                    DrawerIsOpen = true;
                });
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

        private async Task ExecuteOpenModalSortCommand()
        {
            if (IsBusy)
                return;

            try
            {
                await Task.Run(() => 
                {
                    IsBusy = true;
                    LockStates = new double[] { 0, 0.35, 0.55 };
                    FilterSortVisible = true;
                    FilterGenerationVisible = false;
                    FiltersVisible = false;
                    DrawerIsOpen = true;
                });
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

        private async Task ExecuteOpenModalFiltersCommand()
        {
            if (IsBusy)
                return;

            try
            {
                await Task.Run(() => 
                {
                    IsBusy = true;
                    LockStates = new double[] { 0, 0.35, 0.88 };
                    FiltersVisible = true;
                    FilterGenerationVisible = false;
                    FilterSortVisible = false;
                    DrawerIsOpen = true;
                });
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

        private async Task ExecuteCloseModalCommand()
        {
            if (IsBusy)
                return;

            try
            {
                await Task.Run(() =>
                {
                    IsBusy = true;
                    DrawerIsOpen = false;
                });
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

        private async Task ExecuteSelectGenerationCommand(GenerationFilterModel generation)
        {
            Filters.Generations.ForEach((item) => 
            { 
                if(!item.Equals(generation))
                    item.Selected = false;
            });

            generation.Selected = !generation.Selected;

            await Task.Delay(500);
            DrawerIsOpen = false;
        }

        private async Task ExecuteSelectSortCommand(SortFilterModel sort)
        {
            Filters.Orders.ForEach((item) => { item.Selected = false; });
            sort.Selected = true;

            await Task.Delay(500);
            DrawerIsOpen = false;
        }

        private void ExecuteSelectFilterTypeCommand(TypeFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterWeaknessCommand(WeaknessFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterHeightCommand(HeightFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterWeightCommand(WeightFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private async Task ExecuteResetFiltersCommand() 
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Filters.Types = new ObservableRangeCollection<TypeFilterModel>(PokemonHelper.GetFilterTypes());
                Filters.Weaknesses = new ObservableRangeCollection<WeaknessFilterModel>(PokemonHelper.GetFilterWeaknesses());
                Filters.Heights = new ObservableRangeCollection<HeightFilterModel>(PokemonHelper.GetFilterHeights());
                Filters.Weights = new ObservableRangeCollection<WeightFilterModel>(PokemonHelper.GetFilterWeights());
                Filters.NumberRangeMin = 1;
                Filters.NumberRangeMax = 898;

                await Task.Delay(800);
                DrawerIsOpen = false;
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

        private async Task ExecuteApplyFiltersCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Task.Delay(500);
                DrawerIsOpen = false;
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
