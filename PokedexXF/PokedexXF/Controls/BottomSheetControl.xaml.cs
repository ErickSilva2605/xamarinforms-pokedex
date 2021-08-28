using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokedexXF.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomSheetControl : ContentView
    {
        private double _currentPosition = 0;
        public BottomSheetControl()
        {
            InitializeComponent();
        }

        public static BindableProperty SheetMarginProperty = BindableProperty.Create(
            nameof(SheetMargin),
            typeof(Thickness),
            typeof(BottomSheetControl),
            defaultValue: new Thickness(0),
            defaultBindingMode: BindingMode.TwoWay);

        public Thickness SheetMargin
        {
            get { return (Thickness)GetValue(SheetMarginProperty); }
            set { SetValue(SheetMarginProperty, value); OnPropertyChanged(); }
        }

        public static BindableProperty SheetContentProperty = BindableProperty.Create(
            nameof(SheetContent),
            typeof(View),
            typeof(BottomSheetControl),
            defaultValue: default(View),
            defaultBindingMode: BindingMode.TwoWay);

        public View SheetContent
        {
            get { return (View)GetValue(SheetContentProperty); }
            set { SetValue(SheetContentProperty, value); OnPropertyChanged(); }
        }

        private async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    _currentPosition = e.TotalY;
                    break;
                case GestureStatus.Completed:

                    if (_currentPosition > 0)
                        await CloseSheet();

                    break;
            }
        }

        public async Task CloseSheet()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}