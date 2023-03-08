using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using PokedexXF.Handlers;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.UI.Views;

namespace PokedexXF;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddCompatibilityRenderers(typeof(TabView).Assembly);
                handlers.AddCompatibilityRenderers(typeof(TabViewItem).Assembly);
                handlers.AddCompatibilityRenderers(typeof(TouchEffect).Assembly);
            })
            .ConfigureEffects(effects =>
            {
                effects.AddCompatibilityEffects(typeof(VisualFeedbackEffect).Assembly);
                effects.AddCompatibilityEffects(typeof(TouchEffect).Assembly);
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("sf-pro-display-regular.ttf", "FontRegular");
                fonts.AddFont("sf-pro-display-medium.ttf", "FontMedium");
                fonts.AddFont("sf-pro-display-bold.ttf", "FontBold");
            });

        EntryBorderlessHandler.Configure();

        return builder.Build();
    }
}
