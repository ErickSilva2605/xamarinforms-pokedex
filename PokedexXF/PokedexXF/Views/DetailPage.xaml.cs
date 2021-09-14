using PokedexXF.Models;
using PokedexXF.Services;
using PokedexXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(PokemonModel pokemon)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(Navigation, new RestService(), pokemon);
        }
    }
}