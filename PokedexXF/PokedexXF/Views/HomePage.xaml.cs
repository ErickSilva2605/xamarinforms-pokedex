using PokedexXF.Services;
using PokedexXF.ViewModels;

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