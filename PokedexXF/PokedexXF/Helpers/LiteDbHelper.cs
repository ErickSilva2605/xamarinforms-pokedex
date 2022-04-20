using PokedexXF.Models;
using PokedexXF.Services;
using System.Collections.Generic;

namespace PokedexXF.Helpers
{
    public class LiteDbHelper
    {
        public static void UpdateFiltersDataBase(LiteDbService<FiltersModel> db, FiltersModel filters)
        {
            db.DeleteAll();
            db.UpsertItem(filters);
        }

        public static void UpdateResourceListDataBase(LiteDbService<ResourceListModel> db, ResourceListModel resourceList)
        {
            db.DeleteAll();
            db.UpsertItem(resourceList);
        }

        public static void UpdatePokemonListDataBase(LiteDbService<PokemonModel> db, IEnumerable<PokemonModel> pokemons)
        {
            db.DeleteAll();
            foreach (var item in pokemons)
                db.UpsertItem(item);
        }

        public static void UpdatePokemonSpeciesDataBase(LiteDbService<PokemonSpeciesModel> db, PokemonSpeciesModel pokemonSpecie)
        {
            db.UpsertItem(pokemonSpecie);
        }
    }
}
