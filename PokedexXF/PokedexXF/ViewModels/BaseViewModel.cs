using PokedexXF.ObjectModel;

namespace PokedexXF.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        public INavigation Navigation;
        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public virtual void OnDisappearing() { }

        public static bool InternetConnectivity()
        {
            var connectivity = Connectivity.NetworkAccess;
            if (connectivity == NetworkAccess.Internet)
                return true;

            return false;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }
    }
}
