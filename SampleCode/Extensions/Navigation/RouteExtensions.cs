using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class RouteExtensions
{
    public static RouteModel ToModel(this RouteViewModel route)
    {
        return new RouteModel()
        {
            Id = route.Id,
            Name = route.Name,
            RouteAddresses = route.RouteAddresses == null ? null : route.RouteAddresses.ToModels().ToList()
        };
    }
      

    public static RouteViewModel ToViewModel(this RouteModel route)
    {
        return new RouteViewModel()
        {
            Id = route.Id,
            Name = route.Name,
            RouteAddresses = new ObservableCollection<RouteAddressViewModel>(route.RouteAddresses == null ? null : route.RouteAddresses.ToViewModels()),
        };
    }   

    public static IEnumerable<RouteModel> ToModels(this IEnumerable<RouteViewModel> routes)
    {
        return routes.Select(route => route.ToModel());
    }  

    public static IEnumerable<RouteViewModel> ToViewModels(this IEnumerable<RouteModel> routes)
    {
        return routes.Select(routes => routes.ToViewModel());
    }
}