using PokedexXF.Services;
using PokedexXF.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        const uint DURATION_ANIMATION = 1000;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation, new RestService());

            labelTitle.TranslationX = -300;
            frameSearch.TranslationX = -300;
            frameFilterGeneration.TranslationY = -300;
            frameFilterSort.TranslationY = -300;
            frameFilters.TranslationY = -300;
            collectionPokemons.Opacity = 0;
        }

        protected override async void OnAppearing()
        {
            await Task.WhenAll(
                labelTitle.TranslateTo(0, -300, DURATION_ANIMATION, Easing.Linear),
                labelTitle.TranslateTo(0, -150, DURATION_ANIMATION, Easing.Linear),
                labelTitle.TranslateTo(0, 0, DURATION_ANIMATION, Easing.Linear),
                frameSearch.TranslateTo(0, -300, DURATION_ANIMATION, Easing.Linear),
                frameSearch.TranslateTo(0, -150, DURATION_ANIMATION, Easing.Linear),
                frameSearch.TranslateTo(0, 0, DURATION_ANIMATION, Easing.Linear)
            );

            await Task.WhenAll(
                frameFilterGeneration.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
                frameFilterGeneration.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
                frameFilterGeneration.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
            );

            await Task.WhenAll(
                frameFilterSort.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
                frameFilterSort.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
                frameFilterSort.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
            );

            await Task.WhenAll(
                frameFilters.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
                frameFilters.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
                frameFilters.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
            );

            await Task.WhenAll(
                collectionPokemons.FadeTo(0.5, DURATION_ANIMATION, Easing.Linear),
                collectionPokemons.FadeTo(1, DURATION_ANIMATION, Easing.Linear)
            );

            base.OnAppearing();
        }

        private void CustomEntryBorderless_Focused(object sender, FocusEventArgs e)
        {
            frameSearch.BackgroundColor = (Color)Application.Current.Resources["ColorBackgroundPressedInput"];
        }

        private void CustomEntryBorderless_Unfocused(object sender, FocusEventArgs e)
        {
            frameSearch.BackgroundColor = (Color)Application.Current.Resources["ColorBackgroundDefaultInput"];
        }
    }
}