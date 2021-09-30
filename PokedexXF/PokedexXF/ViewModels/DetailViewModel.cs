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
            Pokemon.Locations = new ObservableRangeCollection<PokemonLocationModel>();

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

                if(species.PokedexNumbers.Any())
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
    }
}
