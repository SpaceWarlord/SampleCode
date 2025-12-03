using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.DTO.Navigation;
using SampleCode.Extensions.Navigation;
using SampleCode.Interfaces;
using SampleCode.Services.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation
{
    public partial class RouteAddressPageViewModel : PageViewModel, IPageViewModel<RouteAddressViewModel>
    {
        [ObservableProperty]
        private ObservableCollection<RouteAddressViewModel> _pageItemsList;

        [ObservableProperty]
        private ObservableCollection<AddressViewModel> _addresses;

        private RouteAddressService _routeAddressService { get; set; }

        private AddressService _addressService { get; set; }
        public RouteAddressPageViewModel()
        {
            PageItemsList = new ObservableCollection<RouteAddressViewModel>();
            Addresses = new ObservableCollection<AddressViewModel>();
            _routeAddressService = new RouteAddressService(new Models.SampleDbContext());
            _addressService = new AddressService(new Models.SampleDbContext());
        }

        public async Task AddUpdate(RouteAddressViewModel viewModel)
        {
            await viewModel.AddUpdate(_routeAddressService);
        }

        public async Task LoadData()
        {
            PageItemsList.Clear();
            ObservableCollection<RouteAddressDTO> routeAddressDTOs = await _routeAddressService.GetAll();
            Debug.WriteLine("total routeAddress dtos " + routeAddressDTOs.Count);
            PageItemsList = new ObservableCollection<RouteAddressViewModel>(routeAddressDTOs.ToViewModels());

            Addresses.Clear();
            ObservableCollection<AddressDTO> addressDTOs = await _addressService.GetAll();
            Addresses = new ObservableCollection<AddressViewModel>(addressDTOs.ToViewModels());
            Debug.WriteLine("Total adddresses: " + Addresses.Count);
        }
    }
}