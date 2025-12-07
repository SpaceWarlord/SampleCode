using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation
{
    public partial class RouteAddressPageViewModel : PageViewModel, IPageViewModel<RouteAddressViewModel>
    {
        [ObservableProperty]
        private ObservableCollection<RouteAddressViewModel> _pageItemsList;

        [ObservableProperty]
        private ObservableCollection<AddressViewModel> _addresses;        
        
        public RouteAddressPageViewModel()
        {
            PageItemsList = new ObservableCollection<RouteAddressViewModel>();
            Addresses = new ObservableCollection<AddressViewModel>();                        
        }        

        public async Task LoadData()
        {
            PageItemsList.Clear();            
            PageItemsList = new ObservableCollection<RouteAddressViewModel>(RouteAddressViewModel.GetAll());
            Addresses.Clear();            
            Addresses = new ObservableCollection<AddressViewModel>(AddressViewModel.GetAll());            
        }

        public Task Add(RouteAddressViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(RouteAddressViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(RouteAddressViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}