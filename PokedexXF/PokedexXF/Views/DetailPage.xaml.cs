using PokedexXF.Models;
using PokedexXF.Services;
using PokedexXF.ViewModels;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace PokedexXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        const uint DURATION_ANIMATION_IMAGE = 300;
        const uint DURATION_ANIMATION = 1000;

        public DetailPage(PokemonModel pokemon)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(Navigation, new RestService(), pokemon);

            imagePokemon.TranslationX = -300;
            contentBadgeType.TranslationY = -300;
            labelPokemonNumberName.TranslationY = -300;
            //gradientPatternCircle.Opacity = 0;
            //gradientPokemonName.Opacity = 0;
            //gradientPattern10x5.Opacity = 0;
        }

        protected override async void OnAppearing()
        {
            //await gradientPokemonName.FadeTo(0.3, DURATION_ANIMATION_IMAGE, Easing.Linear);
            //await gradientPokemonName.FadeTo(0.7, DURATION_ANIMATION_IMAGE, Easing.Linear);
            //await gradientPokemonName.FadeTo(1, DURATION_ANIMATION_IMAGE, Easing.Linear);
            await imagePokemon.TranslateTo(-300, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
            await imagePokemon.FadeTo(0.5, DURATION_ANIMATION_IMAGE, Easing.Linear);
            await imagePokemon.TranslateTo(-150, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
            await imagePokemon.TranslateTo(0, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
            await imagePokemon.FadeTo(1, DURATION_ANIMATION_IMAGE, Easing.Linear);
            //await gradientPatternCircle.FadeTo(0.5, DURATION_ANIMATION_IMAGE, Easing.Linear);
            //await gradientPatternCircle.FadeTo(1, DURATION_ANIMATION_IMAGE, Easing.Linear);
            //await gradientPattern10x5.FadeTo(1, DURATION_ANIMATION_IMAGE, Easing.Linear);


            await Task.WhenAll(
                contentBadgeType.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
                contentBadgeType.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
                contentBadgeType.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
            );

            await Task.WhenAll(
                labelPokemonNumberName.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
                labelPokemonNumberName.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
                labelPokemonNumberName.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
            );

            base.OnAppearing();
        }
    }
}