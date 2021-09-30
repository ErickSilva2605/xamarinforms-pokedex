using Flurl;
using Flurl.Http;
using PokedexXF.Helpers;
using PokedexXF.Interfaces;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexXF.Services
{
    public class RestService : IRestService
    {
        public async Task<PokemonModel> GetPokemon(string url)
        {
            try
            {
                var response = await url
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<PokemonModel>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginationModel> GetPokemons(string url)
        {
            try
            {
                PaginationModel response;

                if (string.IsNullOrEmpty(url))
                {
                    response = await Constants.BASE_URL
                        .AppendPathSegment("pokemon")
                        .WithTimeout(TimeSpan.FromSeconds(30))
                        .GetJsonAsync<PaginationModel>();
                }
                else
                {
                    response = await url
                        .WithTimeout(TimeSpan.FromSeconds(30))
                        .GetJsonAsync<PaginationModel>();
                }
                
                return response;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PokemonSpeciesInfoModel> GetPokemonSpecies(string url)
        {
            try
            {
                var response = await url
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<PokemonSpeciesInfoModel>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DamageRelationsModel> GetPokemonDamageRelation(string type)
        {
            try
            {
                var response = await Constants.BASE_URL
                        .AppendPathSegments("type", type)
                        .WithTimeout(TimeSpan.FromSeconds(30))
                        .GetJsonAsync<PokemonDamageRelationsModel>();

                return response.DamageRelations;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<PokemonPokedexDescriptionModel>> GetPokemonLocationDescription(string url)
        {
            try
            {
                var response = await url
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<PokemonPokedexNumberModel>();

                return response.Descriptions;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
