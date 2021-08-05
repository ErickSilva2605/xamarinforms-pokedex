using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Helpers
{
    public class PokemonMock
    {
        public static IList<PokemonModel> GetPokemonMock(int amount)
        {
            IList<PokemonModel> pokemonMockList = new List<PokemonModel>();

            for (int i = 1; i <= amount; i++)
            {
                pokemonMockList.Add(new PokemonModel()
                {
                    Name = "",
                    Id = i,
                    Types = new ObservableRangeCollection<PokemonTypeModel>(GetPokemonTypesMock()),
                    IsBusy = true

                });
            }

            return pokemonMockList;
        }

        private static IList<PokemonTypeModel> GetPokemonTypesMock()
        {
            return new List<PokemonTypeModel>
            {
                new PokemonTypeModel()
                {
                    Slot = 1,
                    Type = new TypeModel() { Name = "Bug", Url = "x" },
                    IsBusy = true
                },
                new PokemonTypeModel()
                {
                    Slot = 1,
                    Type = new TypeModel() { Name = "Bug", Url = "x" },
                    IsBusy = true
                }
            };
        }
    }
}
