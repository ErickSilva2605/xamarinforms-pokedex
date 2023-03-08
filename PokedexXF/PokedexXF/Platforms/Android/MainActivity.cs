﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace PokedexXF;

[Activity(Label = "Pokedex",
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);

        this.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
        this.Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
    }
}
