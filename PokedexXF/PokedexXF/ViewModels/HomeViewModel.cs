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
        private ObservableRangeCollection<PokemonModel> _pokemonsHelp;
        private IEnumerable<PokemonModel> DbPokemons;
        private FiltersModel _filters;
        private string _searchParameter;
        private int _itemTreshold;
        private bool _drawerIsOpen;
        private double[] _lockStates = new double[] { };
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

        public string SearchParameter
        {
            get => _searchParameter;
            set => SetProperty(ref _searchParameter, value);
        }

        public int Offset
        {
            get;
            set;
        }

        public int Amount
        {
            get => Offset + Constants.PAGE_LIMIT;
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

        public ResourceListModel Pages
        {
            get;
            private set;
        }

        public ObservableRangeCollection<PokemonModel> Pokemons
        {
            get => _pokemons;
            set => SetProperty(ref _pokemons, value);
        }

        public ObservableRangeCollection<PokemonModel> PokemonsHelp
        {
            get => _pokemonsHelp;
            set => SetProperty(ref _pokemonsHelp, value);
        }

        public ICommand LoadMorePokemonsCommand { get; }
        public ICommand OpenModalFiltersCommand { get; }
        public ICommand OpenModalSortCommand { get; }
        public ICommand OpenModalGenerationCommand { get; }
        public ICommand CloseModalCommand { get; }
        public ICommand NavigateToDetailCommand { get; }
        public ICommand SelectGenerationCommand { get; }
        public ICommand SelectSortCommand { get; }
        public ICommand SelectFilterTypeCommand { get; }
        public ICommand SelectFilterWeaknessCommand { get; }
        public ICommand SelectFilterHeightCommand { get; }
        public ICommand SelectFilterWeightCommand { get; }
        public ICommand ResetFiltersCommand { get; }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand SearchTextChangedCommand { get; }
        public ICommand SearchCompletedCommand { get; }

        public HomeViewModel(INavigation navigation, IRestService restService) : base(navigation)
        {
            ItemTreshold = 1;
            DrawerIsOpen = false;
            _service = restService;
            _dbService = new LiteDbService<PokemonModel>();
            Pages = new ResourceListModel();
            Pokemons = new ObservableRangeCollection<PokemonModel>();
            PokemonsHelp = new ObservableRangeCollection<PokemonModel>();
            DbPokemons = new List<PokemonModel>();
            LoadMorePokemonsCommand = new Command(async () => await ExecuteLoadMorePokemonsCommand());
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
            SearchTextChangedCommand = new Command(ExecuteSearchTextChangedCommand);
            SearchCompletedCommand = new Command(async () => await ExecuteSearchCompletedCommand());
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Filters = PokemonHelper.GetFilters();
            Pokemons.AddRange(PokemonHelper.GetMockPokemonList(Offset, Amount));
            await LoadPokemonsAsync();
        }

        private async Task LoadPokemonsAsync()
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

                await GetResourcePageAsync($"{Constants.BASE_URL}/{Constants.ENDPOINT_POKEMON}");

                var dbPokemons = _dbService.FindAll();

                if (!dbPokemons.Any() || dbPokemons.Count() < Constants.PAGE_LIMIT)
                {
                    Pokemons = new ObservableRangeCollection<PokemonModel>(await GetPokemonListAsync());

                    if (Pokemons.Any())
                        LiteDbHelper.UpdatePokemonListDataBase(_dbService, Pokemons);
                }
                else
                {
                    await Task.Delay(5000);
                    Pokemons = new ObservableRangeCollection<PokemonModel>(dbPokemons.Take(Constants.PAGE_LIMIT));
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

        private async Task ExecuteLoadMorePokemonsCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Offset += Constants.PAGE_LIMIT;

                Pages.Next = PokemonHelper.RefreshAndValidateNextPage(Offset, Pages.Count, Pages.Next);
                if (string.IsNullOrEmpty(Pages.Next))
                {
                    ItemTreshold = -1;
                    return;
                }

                if (!InternetConnectivity())
                {
                    // TODO - Mensagem dados offline
                    return;
                }

                var mock = PokemonHelper.GetMockPokemonList(Offset, Amount);
                Pokemons.AddRange(mock);

                var dbPokemons = _dbService.FindAll();

                if (!dbPokemons.Any() || dbPokemons.Count() <= Offset)
                {
                    await GetResourcePageAsync(Pages.Next);
                    var pokemonList = await GetPokemonListAsync();
                    Pokemons.RemoveRange(mock);

                    if (pokemonList.Any())
                        Pokemons.AddRange(pokemonList);

                    if (Pokemons.Any())
                        LiteDbHelper.UpdatePokemonListDataBase(_dbService, Pokemons);
                }
                else
                {
                    await Task.Delay(5000);
                    Pokemons.RemoveRange(mock);
                    Pokemons.AddRange(dbPokemons.Skip(Offset).Take(Constants.PAGE_LIMIT));
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

        private async Task GetResourcePageAsync(string url)
        {
            try
            {
                if (!InternetConnectivity())
                    return;

                var pages = await _service.GetResourceAsync<ResourceListModel>(url);

                if (pages == null)
                    return;

                Pages = pages;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task<List<PokemonModel>> GetPokemonListAsync()
        {
            try
            {
                List<PokemonModel> pokemonsList = new List<PokemonModel>();

                foreach (var item in Pages.Results)
                {
                    var pokemon = await _service.GetResourceAsync<PokemonModel>(item.Url);

                    if (pokemon != null)
                    {
                        pokemon.Stats = PokemonHelper.GetFullStats(pokemon.Stats);
                        pokemon.TotalStat = PokemonHelper.GetTotalStats(pokemon.Stats);
                        pokemon.Meters = PokemonHelper.DecimetresToMeters(pokemon.Height);
                        pokemon.Inches = PokemonHelper.MetersToInches(pokemon.Meters);
                        pokemon.Kilograms = PokemonHelper.HectogramsToKilograms(pokemon.Weight);
                        pokemon.Pounds = PokemonHelper.KilogramsToPounds(pokemon.Kilograms);

                        pokemon.TypeDefenses = await GetPokemonTypeDefensesAsync(pokemon);
                        pokemon.Weaknesses = PokemonHelper.GetPokemonWeaknesses(pokemon.TypeDefenses);

                        pokemonsList.Add(pokemon);
                    }
                }

                return pokemonsList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }

            return new List<PokemonModel>();
        }

        private async Task<PokemonModel> GetPokemonAsync(string searchParameter)
        {
            IsBusy = true;
            PokemonModel pokemon = new PokemonModel();
            try
            {
                pokemon = await _service.GetResourceByNameAsync<PokemonModel>(pokemon.ApiEndpoint, searchParameter);

                if (pokemon != null)
                {
                    pokemon.Stats = PokemonHelper.GetFullStats(pokemon.Stats);
                    pokemon.TotalStat = PokemonHelper.GetTotalStats(pokemon.Stats);
                    pokemon.Meters = PokemonHelper.DecimetresToMeters(pokemon.Height);
                    pokemon.Inches = PokemonHelper.MetersToInches(pokemon.Meters);
                    pokemon.Kilograms = PokemonHelper.HectogramsToKilograms(pokemon.Weight);
                    pokemon.Pounds = PokemonHelper.KilogramsToPounds(pokemon.Kilograms);

                    pokemon.TypeDefenses = await GetPokemonTypeDefensesAsync(pokemon);
                    pokemon.Weaknesses = PokemonHelper.GetPokemonWeaknesses(pokemon.TypeDefenses);

                    return pokemon;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return null;
        }

        private async Task<IEnumerable<PokemonTypeDefenseModel>> GetPokemonTypeDefensesAsync(PokemonModel pokemon)
        {
            List<PokemonTypeDefenseModel> typeDefenses = new List<PokemonTypeDefenseModel>();

            try
            {
                List<TypeModel> damageRelations = new List<TypeModel>();
                foreach (var type in pokemon.Types)
                {
                    var damageRelation = await _service.GetResourceByNameAsync<TypeModel>(type.Type.ApiEndpoint, type.Type.Name.ToLower());

                    if (damageRelation != null)
                    {
                        type.Type.DamageRelations = damageRelation.DamageRelations;
                        type.Type.AllTypeRelations = PokemonHelper.GetAllTypeRelations(type.Type.DamageRelations);
                        damageRelations.Add(type.Type);
                    }
                }

                if (damageRelations.Any())
                {
                    foreach (var item in damageRelations[0].AllTypeRelations)
                    {
                        double multiplier;
                        if (damageRelations.Count > 1)
                        {
                            multiplier = PokemonHelper.EffectToMultiplier(item.Effect) *
                            PokemonHelper.EffectToMultiplier(
                                    damageRelations[1].AllTypeRelations
                                    .Where(w => w.Type == item.Type)
                                    .Select(s => s.Effect).FirstOrDefault());
                        }
                        else
                            multiplier = PokemonHelper.EffectToMultiplier(item.Effect);

                        typeDefenses.Add(new PokemonTypeDefenseModel()
                        {
                            Effect = PokemonHelper.MultiplierToEffect(multiplier),
                            Multiplier = PokemonHelper.MultiplierToDescription(multiplier),
                            Type = item.Type
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }

            return typeDefenses;
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
                    LockStates = new double[] { 0, 0.44, 0.9 };
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
                    LockStates = new double[] { 0, 0.44, 0.62 };
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
                    LockStates = new double[] { 0, 0.44, 0.97 };
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
                if (!item.Equals(generation))
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

        private void ExecuteSearchTextChangedCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchParameter))
                ClearSearch();
        }

        private async Task ExecuteSearchCompletedCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchParameter))
            {
                ClearSearch();
                return;
            }

            ItemTreshold = -1;

            if (!DbPokemons.Any())
                DbPokemons = _dbService.FindAll();

            if (!PokemonsHelp.Any())
                PokemonsHelp = new ObservableRangeCollection<PokemonModel>(Pokemons);

            Pokemons = new ObservableRangeCollection<PokemonModel>(
                DbPokemons.Where(w =>
                w.Id.ToString().Contains(SearchParameter) ||
                w.NameUpperCase.Contains(SearchParameter.ToUpper()))
            );

            if (Pokemons.Any())
                return;

            var mock = PokemonHelper.GetMockPokemon();
            Pokemons.Add(mock);

            var pokemon = await GetPokemonAsync(SearchParameter);

            await Task.Delay(1000);
            Pokemons.Remove(mock);

            if (pokemon != null)
                Pokemons.Add(pokemon);
        }

        private void ClearSearch()
        {
            if (PokemonsHelp.Any())
            {
                Pokemons = new ObservableRangeCollection<PokemonModel>(PokemonsHelp);
                PokemonsHelp = new ObservableRangeCollection<PokemonModel>();
            }

            ItemTreshold = 1;
            SearchParameter = string.Empty;
            DbPokemons = new List<PokemonModel>();
        }
    }
}
