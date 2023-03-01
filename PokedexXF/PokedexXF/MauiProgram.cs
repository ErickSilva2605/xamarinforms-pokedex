namespace PokedexXF;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("sf-pro-display-regular.ttf", "FontRegular");
                fonts.AddFont("sf-pro-display-medium.ttf", "FontMedium");
                fonts.AddFont("sf-pro-display-bold.ttf", "FontBold");
            });

        return builder.Build();
    }
}
