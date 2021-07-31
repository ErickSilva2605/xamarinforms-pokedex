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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CustomEntryBorderless_Focused(object sender, FocusEventArgs e)
        {
            cardSearch.BackgroundColor = Color.FromHex("#E2E2E2");
        }

        private void CustomEntryBorderless_Unfocused(object sender, FocusEventArgs e)
        {
            cardSearch.BackgroundColor = Color.FromHex("#F2F2F2");
        }
    }
}