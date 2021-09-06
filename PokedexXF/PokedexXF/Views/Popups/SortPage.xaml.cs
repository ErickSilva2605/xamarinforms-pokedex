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
        public SortPage()
        {
            InitializeComponent();
            BindingContext = new SortViewModel(Navigation);
        }
    }
}