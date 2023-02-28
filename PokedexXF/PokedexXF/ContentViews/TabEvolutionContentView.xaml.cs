using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Xamarin.Forms.Xaml;

namespace PokedexXF.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabEvolutionContentView : ContentView
    {
        public TabEvolutionContentView()
        {
            InitializeComponent();
        }
    }
}