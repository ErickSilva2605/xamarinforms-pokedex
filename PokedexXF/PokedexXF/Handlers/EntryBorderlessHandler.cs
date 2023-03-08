using PokedexXF.Controls;

namespace PokedexXF.Handlers;

public static class EntryBorderlessHandler
{
    public static void Configure()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntryBorderless), (handler, view) =>
        {
            if (view is CustomEntryBorderless)
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
    }
}
