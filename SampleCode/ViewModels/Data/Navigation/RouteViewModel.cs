using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Extensions.Navigation;
using SampleCode.Services.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class RouteViewModel : DataViewModel
    {
        [ObservableProperty]
        [Required]
        [NotNull]
        private string _name;

        [ObservableProperty]
        [Required]
        [NotNull]
        private ObservableCollection<RouteAddressViewModel>? _routeAddresses;


        [ObservableProperty]
        private float _distance;

        public RouteViewModel()
        {

        }
        public RouteViewModel(int id, string name, ObservableCollection<RouteAddressViewModel> routeAddresses, float distance)
        {
            Id = id;
            Name = name;
            RouteAddresses = routeAddresses;
            Distance = distance;
        }

        public async Task AddUpdate(RouteService route)
        {
            Debug.WriteLine("Called Save Async");
            IsModified = false;
            if (IsNew)
            {
                Debug.WriteLine("its new");
                IsNew = false;
                int result = await route.AddUpdate(this.ToDto());
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