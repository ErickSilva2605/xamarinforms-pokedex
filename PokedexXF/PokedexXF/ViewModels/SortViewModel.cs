using PokedexXF.Enums;
using PokedexXF.Models;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class SortViewModel : BaseViewModel
    {
        private FiltersModel _filters;

        public FiltersModel Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        public Command<SortFilterModel> SelectSortCommand { get; set; }

        public SortViewModel(INavigation navigation) : base(navigation)
        {
            Filters = new FiltersModel();
            SelectSortCommand = new Command<SortFilterModel>((sort) => ExecuteSelectSortCommand(sort));
            InitFilter();
        }

        private void InitFilter()
        {
            Filters.Orders = new ObservableRangeCollection<SortFilterModel>(GetOrders());
        }

        private IList<SortFilterModel> GetOrders()
        {
            return new List<SortFilterModel>()
            {
                new SortFilterModel { Sort = SortEnum.Ascending, Selected = true },
                new SortFilterModel { Sort = SortEnum.Descending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalAscending },
                new SortFilterModel { Sort = SortEnum.AlphabeticalDescending }
            };
        }

        private void ExecuteSelectSortCommand(SortFilterModel sort)
        {
            foreach (var item in Filters.Orders)
                item.Selected = false;

            sort.Selected = true;
        }
    }
}
