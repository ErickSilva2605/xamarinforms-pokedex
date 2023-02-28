using LiteDB;
using PokedexXF.Views;
using System.Globalization;
using System.IO;
using Xamarin.Essentials;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace PokedexXF
{
    public partial class App : Application
    {
        public static LiteDatabase Database = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "pokedex.db"));

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
