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
    public partial class RoutePageViewModel : PageViewModel, IPageViewModel<RouteViewModel>
    {
        [ObservableProperty]
        private ObservableCollection<RouteViewModel> _pageItemsList;

        [ObservableProperty]
        private RouteAddressPageViewModel _subPageViewModel;

        private RouteService _routeService { get; set; }


        public RoutePageViewModel()
        {
            PageItemsList = new ObservableCollection<RouteViewModel>();
            SubPageViewModel = new RouteAddressPageViewModel();
            _routeService = new RouteService(new Models.SampleDbContext());
        }

        public async Task AddUpdate(RouteViewModel viewModel)
        {
            await viewModel.AddUpdate(_routeService);
        }

        public async Task LoadData()
        {
            PageItemsList.Clear();
            ObservableCollection<RouteDTO> routeDTOs = await _routeService.GetAll();
            Debug.WriteLine("total route dtos " + routeDTOs.Count);
            PageItemsList = new ObservableCollection<RouteViewModel>(routeDTOs.ToViewModels());
        }
    }
}