using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using SampleCode.Maps;
using SampleCode.Services.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
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

        private RouteService _routeService;

        public RoutePageViewModel(RouteService service)        
        {
            _routeService = service;
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
            /*
            using (var a = new SampleDbContext())
            {
                a.RouteAddresses.ExecuteDelete();
            }
            */
            List<RouteModel> b = new List<RouteModel>(await _routeService.GetAll());
            //var b = RouteViewModel.GetAll();
            RouteMap map = new RouteMap();
            foreach (var item in b)
            {
                //PageItemsList.Add(new RouteViewModel(item));
                //PageItemsList.Add(RouteViewModel.Create(item));
                PageItemsList.Add(map.MapFromModel(item, true));
            }            
        }

        public Task Delete(RouteViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}