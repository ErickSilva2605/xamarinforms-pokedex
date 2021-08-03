using PokedexXF.Interfaces;
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
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation, new RestService());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await OnAppearingAsync();
        }

        private async Task OnAppearingAsync()
        {
            if (BindingContext is IInitializeAsync viewModel)
                await viewModel.InitializeAsync();
        }

        private void CustomEntryBorderless_Focused(object sender, FocusEventArgs e)
        {
            cardSearch.BackgroundColor = (Color)Application.Current.Resources["ColorBackgroundPressedInput"];
        }

        private void CustomEntryBorderless_Unfocused(object sender, FocusEventArgs e)
        {
            cardSearch.BackgroundColor = (Color)Application.Current.Resources["ColorBackgroundDefaultInput"];
        }
    }
}