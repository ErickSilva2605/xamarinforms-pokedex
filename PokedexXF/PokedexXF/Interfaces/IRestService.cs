using PokedexXF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexXF.Interfaces
{
    public interface IRestService
    {
        Task<PaginationModel> GetPokemons(string url);
        Task<PokemonModel> GetPokemon(string url);
        Task<PokemonSpeciesInfoModel> GetPokemonSpecies(string url);
        Task<DamageRelationsModel> GetPokemonDamageRelation(string type);
        Task<IList<PokemonPokedexDescriptionModel>> GetPokemonLocationDescription(string url);
        Task<ChainModel> GetPokemonChain(string url);
    }
}
