using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Extensions.Navigation;
using SampleCode.Services.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class RouteAddressViewModel : DataViewModel
    {
        [ObservableProperty]
        private RouteViewModel _route;

        [ObservableProperty]
        private AddressViewModel _address;

        [ObservableProperty]
        private int _order;

        public RouteAddressViewModel()
        {

        }

        public RouteAddressViewModel(RouteViewModel route, AddressViewModel address, int order)
        {
            Route = route;
            Address = address;
            Order = order;
        }

        public async Task AddUpdate(RouteAddressService routeAddress)
        {
            Debug.WriteLine("Called Save Async");
            IsModified = false;
            if (IsNew)
            {
                Debug.WriteLine("its new");
                IsNew = false;
                int result = await routeAddress.AddUpdate(this.ToDto());
                if (result != 0)
                {
                    Id = result;
                }
                else
                {
                    Debug.WriteLine("Error Adding");
                }
            }
        }
    }
}