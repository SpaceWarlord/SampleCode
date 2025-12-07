using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation
{
    public partial class RoutePageViewModel : PageViewModel, IPageViewModel<RouteViewModel>
    {
        [ObservableProperty]
        private ObservableCollection<RouteViewModel> _pageItemsList;

        [ObservableProperty]
        private RouteAddressPageViewModel _subPageViewModel;       

        public RoutePageViewModel()
        {
            PageItemsList = new ObservableCollection<RouteViewModel>();
            SubPageViewModel = new RouteAddressPageViewModel();            
        }

        public async Task Add(RouteViewModel viewModel)
        {
            await viewModel.Add();
        }

        public async Task Update(RouteViewModel viewModel)
        {
            await viewModel.Update();
        }

        public async Task LoadData()
        {
            PageItemsList.Clear();                        
            PageItemsList = new ObservableCollection<RouteViewModel>(RouteViewModel.GetAll());
        }

        public Task Delete(RouteViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}