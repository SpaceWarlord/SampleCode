using CommunityToolkit.Mvvm.ComponentModel;
using Models.Navigation;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation;

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
        IQueryable<RouteAddressModel> a = RouteAddressViewModel.GetAll();
        
        foreach (RouteAddressModel item in a)
        {
            PageItemsList.Add(new RouteAddressViewModel(item));
            /*
            PageItemsList.Add(new RouteAddressViewModel(
            new RouteViewModel(item.Route.Id, item.Route.Name, 
            
            , item.Route.Distance),              
            new AddressViewModel(item.Address.Id, item.Address.Name, item.Address.UnitNum, item.Address.StreetNum, item.Address.StreetName,
                new StreetTypeViewModel(item.Address.StreetType.Id, item.Address.StreetType.Code, item.Address.StreetType.Name, item.Address.StreetType.Common),
                new SuburbViewModel(item.Address.Suburb.Id, item.Address.Suburb.Name, item.Address.Suburb.PostCode), item.Address.City, item.Address.GPS));
            */
        }
        //PageItemsList = new ObservableCollection<RouteAddressViewModel>(RouteAddressViewModel.GetAll());
        Addresses.Clear();            
        Addresses = new ObservableCollection<AddressViewModel>(AddressViewModel.GetAll());            
    }

    public async Task Add(RouteAddressViewModel viewModel)
    {
        await viewModel.Add();
    }

    public async Task Update(RouteAddressViewModel viewModel)
    {
        await viewModel.Update();
    }

    public async Task Delete(RouteAddressViewModel viewModel)
    {
        throw new System.NotImplementedException();
    }
}