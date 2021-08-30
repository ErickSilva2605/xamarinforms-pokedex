using PokedexXF.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexXF.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortPage : PopupPage
    {
        private Button _lastSelected;
        public SortPage()
        {
            InitializeComponent();
            BindingContext = new SortViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SelectFirstButton();
        }

        private void SelectFirstButton()
        {
            var index = 0;
            foreach (var view in gridSort.Children)
            {
                if (view is Button button)
                {
                    if (index++ == 0)
                    {
                        GoToStateSelected(button);
                        continue;
                    }

                    GoToStateUnSelected(button);
                }
            }
        }

        private void SortClicked(object sender, EventArgs e)
        {
            GoToStateUnSelected(_lastSelected);
            GoToStateSelected((Button)sender);
        }

        private void GoToStateSelected(Button button)
        {
            _lastSelected = button;
            VisualStateManager.GoToState(button, "Selected");
        }

        private void GoToStateUnSelected(Button button)
        {
            VisualStateManager.GoToState(button, "UnSelected");
        }
    }
}