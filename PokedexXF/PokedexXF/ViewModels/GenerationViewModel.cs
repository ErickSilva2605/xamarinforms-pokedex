using PokedexXF.Enums;
using PokedexXF.Models;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace PokedexXF.ViewModels
{
    public class GenerationViewModel : BaseViewModel
    {
        private FiltersModel _filters;

        public FiltersModel Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        public Command<GenerationFilterModel> SelectGenerationCommand { get; set; }
        public GenerationViewModel(INavigation navigation) : base(navigation)
        {
            Filters = new FiltersModel();
            SelectGenerationCommand = new Command<GenerationFilterModel>((generation) => ExecuteSelectGenerationCommand(generation));
            InitFilter();
        }

        private void InitFilter()
        {
            Filters.Generations = new ObservableRangeCollection<GenerationFilterModel>(GetGenerations());
        }

        private IList<GenerationFilterModel> GetGenerations()
        {
            return new List<GenerationFilterModel>()
            {
                new GenerationFilterModel { Generation = GenerationEnum.GenerationOne },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationTwo },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationThree },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationFour },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationFive },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationSix },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationSeven },
                new GenerationFilterModel { Generation = GenerationEnum.GenerationEight }
            };
        }

        private void ExecuteSelectGenerationCommand(GenerationFilterModel generation)
        {
            foreach (var item in Filters.Generations)
                item.Selected = false;

            generation.Selected = !generation.Selected;
        }
    }
}
