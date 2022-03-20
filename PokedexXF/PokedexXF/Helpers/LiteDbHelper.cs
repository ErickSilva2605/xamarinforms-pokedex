using PokedexXF.Models;
using PokedexXF.Services;
using System.Collections.Generic;

namespace PokedexXF.Helpers
{
    public class LiteDbHelper
    {
        public static void UpdateDataBase(LiteDbService<PokemonModel> db, IEnumerable<PokemonModel> pokemons)
        {
            db.DeleteAll();
            foreach (var item in pokemons)
                db.UpsertItem(item);
        }
    }
}
