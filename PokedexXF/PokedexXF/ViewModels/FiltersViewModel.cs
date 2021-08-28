using PokedexXF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PokedexXF.ViewModels
{
    public class FiltersViewModel : BaseViewModel
    {
        private FiltersModel _filters;

        public FiltersModel Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        public Command<TypeFilterModel> SelectFilterTypeCommand { get; set; }
        public Command<WeaknessFilterModel> SelectFilterWeaknessCommand { get; set; }
        public Command<HeightFilterModel> SelectFilterHeightCommand { get; set; }
        public Command<WeightFilterModel> SelectFilterWeightCommand { get; set; }

        public FiltersViewModel(INavigation navigation) : base(navigation)
        {
            Filters = new FiltersModel();
            SelectFilterTypeCommand = new Command<TypeFilterModel>((type) => ExecuteSelectFilterTypeCommand(type));
            SelectFilterWeaknessCommand = new Command<WeaknessFilterModel>((type) => ExecuteSelectFilterWeaknessCommand(type));
            SelectFilterHeightCommand = new Command<HeightFilterModel>((type) => ExecuteSelectFilterHeightCommand(type));
            SelectFilterWeightCommand = new Command<WeightFilterModel>((type) => ExecuteSelectFilterWeightCommand(type));
            InitFilters();
        }

        private void InitFilters()
        {
            Filters.Types = new ObservableRangeCollection<TypeFilterModel>(GetTypes());
            Filters.Weaknesses = new ObservableRangeCollection<WeaknessFilterModel>(GetWeaknesses());
            Filters.Heights = new ObservableRangeCollection<HeightFilterModel>(GetHeights());
            Filters.Weights = new ObservableRangeCollection<WeightFilterModel>(GetWeights());
            Filters.NumberRangeMin = 1;
            Filters.NumberRangeMax = 898;
        }

        private IList<TypeFilterModel> GetTypes()
        {
            return new List<TypeFilterModel>()
            {
                new TypeFilterModel { Type = Enums.TypeEnum.Bug},
                new TypeFilterModel { Type = Enums.TypeEnum.Dark},
                new TypeFilterModel { Type = Enums.TypeEnum.Dragon},
                new TypeFilterModel { Type = Enums.TypeEnum.Electric},
                new TypeFilterModel { Type = Enums.TypeEnum.Fairy},
                new TypeFilterModel { Type = Enums.TypeEnum.Fighting},
                new TypeFilterModel { Type = Enums.TypeEnum.Fire},
                new TypeFilterModel { Type = Enums.TypeEnum.Flying},
                new TypeFilterModel { Type = Enums.TypeEnum.Ghost},
                new TypeFilterModel { Type = Enums.TypeEnum.Grass},
                new TypeFilterModel { Type = Enums.TypeEnum.Ground},
                new TypeFilterModel { Type = Enums.TypeEnum.Ice},
                new TypeFilterModel { Type = Enums.TypeEnum.Poison},
                new TypeFilterModel { Type = Enums.TypeEnum.Psychic},
                new TypeFilterModel { Type = Enums.TypeEnum.Rock},
                new TypeFilterModel { Type = Enums.TypeEnum.Steel},
                new TypeFilterModel { Type = Enums.TypeEnum.Water}
            };
        }

        private IList<WeaknessFilterModel> GetWeaknesses()
        {
            return new List<WeaknessFilterModel>()
            {
                new WeaknessFilterModel { Type = Enums.TypeEnum.Bug},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Dark},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Dragon},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Electric},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Fairy},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Fighting},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Fire},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Flying},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Ghost},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Grass},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Ground},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Ice},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Poison},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Psychic},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Rock},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Steel},
                new WeaknessFilterModel { Type = Enums.TypeEnum.Water}
            };
        }

        private IList<HeightFilterModel> GetHeights()
        {
            return new List<HeightFilterModel>
            {
                new HeightFilterModel{ Height = Enums.HeightEnum.Short },
                new HeightFilterModel{ Height = Enums.HeightEnum.Medium },
                new HeightFilterModel{ Height = Enums.HeightEnum.Tall }
            };
        }

        private IList<WeightFilterModel> GetWeights()
        {
            return new List<WeightFilterModel>
            {
                new WeightFilterModel{ Weight = Enums.WeightEnum.Light },
                new WeightFilterModel{ Weight = Enums.WeightEnum.Normal },
                new WeightFilterModel{ Weight = Enums.WeightEnum.Heavy }
            };
        }

        private void ExecuteSelectFilterTypeCommand(TypeFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterWeaknessCommand(WeaknessFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterHeightCommand(HeightFilterModel type)
        {
            type.Selected = !type.Selected;
        }

        private void ExecuteSelectFilterWeightCommand(WeightFilterModel type)
        {
            type.Selected = !type.Selected;
        }

    }
}
