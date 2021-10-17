using PokedexXF.Enums;
using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using PokedexXF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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

        public ICommand NavigateToBackCommand { get; private set; }

        public DetailViewModel(INavigation navigation, IRestService restService, PokemonModel pokemon) : base(navigation)
        {
            _service = restService;
            _dbService = new LiteDbService<PokemonModel>();
            Pokemon = pokemon;
            Pokemon.Locations = new ObservableRangeCollection<PokemonLocationModel>();

            NavigateToBackCommand = new Command(async () => await ExecuteNavigateToBackCommand());
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Pokemon.Weaknesses.Clear();
                Pokemon.TypeDefenses.Clear();

                if (!InternetConnectivity())
                {
                    // TODO - Mensagem dados offline
                    return;
                }

                await GetPokemonSpecies();
                await GetPokemonDamageRelations();
                CalculateStats();
                CalculateCatchRateProbability();
                CalculateEggSteps();
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

        private async Task GetPokemonSpecies()
        {
            try
            {
                var species = await _service.GetPokemonSpecies(Pokemon.Species.Url);

                if (species == null)
                    return;

                await GetPokemonChain(species.EvolutionChain.Url);

                if (species.PokedexNumbers.Any())
                {
                    foreach (var item in species.PokedexNumbers)
                        await GetPokemonLocationDescription(item);
                }

                Pokemon.BaseHappiness = species.BaseHappiness;
                Pokemon.CaptureRate = species.CaptureRate;
                Pokemon.GenderRate = PokemonHelper.GenderRateToDescription(species.GenderRate);
                Pokemon.GrowthRate = species.GrowthRate.Name;
                Pokemon.HatchCounter = species.HatchCounter;

                Pokemon.EggGroups = string.Join(", ", species.EggGroups.Select(s => s.Name));

                Pokemon.FlavorText = species.FlavorTextEntries
                    .Where(w => w.Language.Name == "en" && w.Version.Name == "ruby")
                    .Select(s => s.FlavorText)
                    .FirstOrDefault()
                    .Replace('\n', ' ')
                    .Replace('\f', ' ');

                Pokemon.Genus = species.Genera
                    .Where(w => w.Language.Name == "en")
                    .Select(s => s.Genus).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task GetPokemonDamageRelations()
        {
            try
            {
                List<PokemonDamageRelationsModel> damageRelations = new List<PokemonDamageRelationsModel>();
                foreach (var type in Pokemon.Types)
                {
                    var damageRelation = await _service.GetPokemonDamageRelation(type.Type.Name.ToLower());

                    TypeEnum typeDefense;
                    if (Enum.TryParse(type.Type.Name, out TypeEnum typeEnum))
                        typeDefense = typeEnum;
                    else
                        typeDefense = TypeEnum.Undefined;

                    if (damageRelation != null)
                    {

                        List<TypeRelationModel> allTypeRelations = new List<TypeRelationModel>();
                        var types = Enum.GetValues(typeof(TypeEnum)).Cast<int>().ToList();

                        foreach (var typeItem in types)
                        {
                            if ((TypeEnum)typeItem == TypeEnum.Undefined)
                                continue;

                            TypeRelationModel typeRelation = new TypeRelationModel();

                            typeRelation.Type = (TypeEnum)typeItem;

                            if (damageRelation.DoubleDamageFrom.Any() && damageRelation.DoubleDamageFrom.Any(x => x.Type == typeRelation.Type))
                                typeRelation.Effect = EffectEnum.SuperEffective;
                            else if (damageRelation.HalfDamageFrom.Any() && damageRelation.HalfDamageFrom.Any(x => x.Type == typeRelation.Type))
                                typeRelation.Effect = EffectEnum.NotVeryEffective;
                            else if (damageRelation.NoDamageFrom.Any() && damageRelation.NoDamageFrom.Any(x => x.Type == typeRelation.Type))
                                typeRelation.Effect = EffectEnum.NoEffect;
                            else
                                typeRelation.Effect = EffectEnum.Normal;

                            allTypeRelations.Add(typeRelation);
                        }

                        damageRelations.Add(new PokemonDamageRelationsModel()
                        {
                            TypeDefense = typeDefense,
                            DamageRelations = damageRelation,
                            AllTypeRelations = new ObservableRangeCollection<TypeRelationModel>(allTypeRelations)
                        });
                    }
                }

                if (damageRelations.Any())
                {
                    if (damageRelations.Count > 1)
                    {
                        foreach (var item in damageRelations[0].AllTypeRelations)
                        {
                            var multiplier = PokemonHelper.EffectToMultiplier(item.Effect) *
                                PokemonHelper.EffectToMultiplier(
                                        damageRelations[1].AllTypeRelations
                                        .Where(w => w.Type == item.Type)
                                        .Select(s => s.Effect).FirstOrDefault()
                                    );

                            Pokemon.TypeDefenses.Add(new PokemonTypeDefenseModel()
                            {
                                Effect = PokemonHelper.MultiplierToEffect(multiplier),
                                Multiplier = PokemonHelper.MultiplierToDescription(multiplier),
                                Type = item.Type
                            });
                        }
                    }
                    else
                    {
                        foreach (var item in damageRelations[0].AllTypeRelations)
                        {
                            var multiplier = PokemonHelper.EffectToMultiplier(item.Effect);

                            Pokemon.TypeDefenses.Add(new PokemonTypeDefenseModel()
                            {
                                Effect = PokemonHelper.MultiplierToEffect(multiplier),
                                Multiplier = PokemonHelper.MultiplierToDescription(multiplier),
                                Type = item.Type
                            });
                        }
                    }
                }

                if (Pokemon.TypeDefenses != null)
                    Pokemon.Weaknesses.AddRange(Pokemon.TypeDefenses.Where(w => w.Effect == EffectEnum.SuperEffective).ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task GetPokemonLocationDescription(PokemonPokedexNumberModel pokedexNumber)
        {
            try
            {
                var descriptions = await _service.GetPokemonLocationDescription(pokedexNumber.Pokedex.Url);

                if (!descriptions.Any())
                    return;

                Pokemon.Locations.Add(new PokemonLocationModel()
                {
                    EntryNumber = pokedexNumber.EntryNumber,
                    Description = $"({descriptions.Where(w => w.Language.Name == "en").Select(s => s.description).FirstOrDefault()})"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task GetPokemonChain(string url)
        {
            try
            {
                var chain = await _service.GetPokemonChain(url);

                if (chain == null)
                    return;

                List<EvolutionModel> evolutions = new List<EvolutionModel>();

                if (chain.EvolvesTo.Any())
                {
                    var pokemonRoot = await GetEvolutionPokemon(chain);

                    foreach (var item in chain.EvolvesTo)
                    {
                        EvolutionModel evolutionOne = new EvolutionModel();
                        evolutionOne.Name = pokemonRoot.Name;
                        evolutionOne.Id = pokemonRoot.Id;
                        evolutionOne.Image = pokemonRoot.Sprites.Other.OfficialArtwork.FrontDefault;
                        evolutionOne.HasEvolution = true;
                        evolutionOne.EnvolvesToMinLevel = item.EvolutionDetails.Select(s => s.MinLevel).FirstOrDefault();
                        evolutionOne.EnvolvesToName = item.Species.Name;

                        var pokemonEvolutionOne = await GetEvolutionPokemon(item);

                        evolutionOne.EnvolvesToImage = pokemonEvolutionOne.Sprites.Other.OfficialArtwork.FrontDefault;
                        evolutionOne.EnvolvesToId = pokemonEvolutionOne.Id;

                        evolutions.Add(evolutionOne);

                        if (item.EvolvesTo.Any())
                        {
                            foreach (var envolves in item.EvolvesTo)
                            {
                                EvolutionModel evolutionTwo = new EvolutionModel();
                                evolutionTwo.Id = evolutionOne.EnvolvesToId;
                                evolutionTwo.Name = item.Species.Name;
                                evolutionTwo.Image = evolutionOne.EnvolvesToImage;
                                evolutionTwo.HasEvolution = true;
                                evolutionTwo.EnvolvesToMinLevel = envolves.EvolutionDetails.Select(s => s.MinLevel).FirstOrDefault();
                                evolutionTwo.EnvolvesToName = envolves.Species.Name;

                                var pokemonEvolution = await GetEvolutionPokemon(envolves);

                                evolutionTwo.EnvolvesToImage = pokemonEvolution.Sprites.Other.OfficialArtwork.FrontDefault;
                                evolutionTwo.EnvolvesToId = pokemonEvolution.Id;

                                evolutions.Add(evolutionTwo);
                            }
                        }
                    }
                }
                else
                {
                    evolutions.Add(new EvolutionModel()
                    {
                        Name = Pokemon.Name,
                        Image = Pokemon.Sprites.Other.OfficialArtwork.FrontDefault,
                        HasEvolution = false
                    });
                }

                Pokemon.Evolutions = new ObservableRangeCollection<EvolutionModel>(evolutions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }
        }

        private async Task<PokemonModel> GetEvolutionPokemon(ChainModel evolution)
        {
            PokemonModel pokemon = new PokemonModel();

            try
            {

                var id = evolution.Species.Url.Remove(evolution.Species.Url.Length - 1).Split('/')[6];

                pokemon = _dbService.FindAll().Where(s => s.Id == Convert.ToInt32(id)).FirstOrDefault();

                if (pokemon == null)
                    pokemon = await _service.GetPokemon($"{Constants.BASE_URL}pokemon/{id}");

                return pokemon;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro", ex.Message);
            }

            return pokemon;
        }

        private async Task ExecuteNavigateToBackCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Navigation.PopAsync();
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

        private void CalculateStats()
        {
            Pokemon.TotalStat = 0;
            foreach (var item in Pokemon.Stats)
            {
                if (item.Stat.Name.ToLower() == "hp")
                {
                    item.MaxStat = (((2 * item.BaseStat) + Constants.IV_MAX + (Constants.EV_MAX / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + Constants.POKEMON_MAX_LEVEL + 10;
                    item.MinStat = (((2 * item.BaseStat) + Constants.IV_MIN + (Constants.EV_MIN / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + Constants.POKEMON_MAX_LEVEL + 10;
                }
                else
                {
                    item.MaxStat = (int)(((((2 * item.BaseStat) + Constants.IV_MAX + (Constants.EV_MAX / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + 5) * Constants.NATURE_MAX);
                    item.MinStat = (int)(((((2 * item.BaseStat) + Constants.IV_MIN + (Constants.EV_MIN / 4)) * Constants.POKEMON_MAX_LEVEL / 100) + 5) * Constants.NATURE_MIN);
                }

                item.PercentageStat = (double)item.BaseStat / ((item.MaxStat + item.MinStat) / 2);
                Pokemon.TotalStat += item.BaseStat;
            }
        }

        private void CalculateCatchRateProbability()
        {
            int hp = Pokemon.Stats.Where(w => w.Stat.Name.ToLower() == "hp").Select(s => s.BaseStat).FirstOrDefault();
            double alpha = (double)(((3 * hp) - (2 * hp)) * Pokemon.CaptureRate * 1 / (3 * hp)) * 1;
            Pokemon.CaptureProbability = (alpha/255) * 100;
        }

        private void CalculateEggSteps()
        {
            Pokemon.MaxSteps = Pokemon.HatchCounter * Constants.EGG_CYCLE_STEPS;
            Pokemon.MinSteps = Pokemon.MaxSteps - (Constants.EGG_CYCLE_STEPS - 1);
        }
    }
}
