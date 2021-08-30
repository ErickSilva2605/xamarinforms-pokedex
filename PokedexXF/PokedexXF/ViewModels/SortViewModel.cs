using PokedexXF.Enums;
using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Command<string> SelectSortCommand { get; set; }

        public SortViewModel(INavigation navigation) : base(navigation)
        {
            Filters = new FiltersModel();
            Filters.Sort = SortEnum.Ascending;

            SelectSortCommand = new Command<string>((sort) => ExecuteSelectSortCommand(sort));
        }

        private void ExecuteSelectSortCommand(string sort)
        {
                Filters.Sort = (SortEnum)Convert.ToInt32(sort);
        }
    }
}
