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
            await Task.Delay(2000);

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
            await Task.Delay(2000);

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
    }
}
