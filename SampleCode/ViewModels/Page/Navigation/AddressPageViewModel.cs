using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using SampleCode.DTO.Navigation;
using SampleCode.Extensions.Navigation;
using SampleCode.Services.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation
{
    public partial class AddressPageViewModel : PageViewModel
    {
        [ObservableProperty]
        private ObservableCollection<AddressViewModel> _pageItemsList;

        [ObservableProperty]
        private ObservableCollection<StreetTypeViewModel> _streetTypes;

        [ObservableProperty]
        private ObservableCollection<SuburbViewModel> _suburbs;
        private AddressService _addressService { get; set; }
        private StreetTypeService _streetTypeService { get; set; }
        private SuburbService _suburbService { get; set; }

        public AddressPageViewModel()
        {
            Debug.WriteLine("-- AddressPageViewModel Constructor--");
            PageItemsList = new ObservableCollection<AddressViewModel>();
            StreetTypes = new ObservableCollection<StreetTypeViewModel>();
            Suburbs = new ObservableCollection<SuburbViewModel>();

            _addressService = new AddressService(new SampleDbContext());
            _streetTypeService = new StreetTypeService(new SampleDbContext());
            _suburbService = new SuburbService(new SampleDbContext());
        }

        public async Task LoadData()
        {
            Debug.WriteLine("-- LoadData --");

            ObservableCollection<AddressDTO> addressDTOs = await _addressService.GetAll();
            PageItemsList.Clear();
            PageItemsList = new ObservableCollection<AddressViewModel>(addressDTOs.ToViewModels());

            ObservableCollection<StreetTypeDTO> streetTypeDTOs = await _streetTypeService.GetAll();
            StreetTypes.Clear();
            StreetTypes = new ObservableCollection<StreetTypeViewModel>(streetTypeDTOs.ToViewModels());

            ObservableCollection<SuburbDTO> suburbDTOs = await _suburbService.GetAll();
            Suburbs.Clear();
            Suburbs = new ObservableCollection<SuburbViewModel>(suburbDTOs.ToViewModels());
        }

        public async Task AddUpdate(AddressViewModel viewModel)
        {
            Debug.WriteLine("-- AddUpdate --");
            await viewModel.AddUpdate(_addressService);
        }        
    }
}