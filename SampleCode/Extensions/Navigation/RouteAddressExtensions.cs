using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class RouteAddressExtensions
{    
    public static RouteAddressModel ToModel(this RouteAddressViewModel vm)
    {
        return new RouteAddressModel()
        {
            Route = vm.Route.ToModel(),
            Address = vm.Address.ToModel(),
            Order = vm.Order
        };
    }
    
    public static RouteAddressViewModel ToViewModel(this RouteAddressModel model)
    {
        return new RouteAddressViewModel(model.Route.ToViewModel(), model.Address.ToViewModel(), model.Order);
    }      

    public static IEnumerable<RouteAddressModel> ToModels(this IEnumerable<RouteAddressViewModel> vms)
    {
        return vms.Select(routeAddress => routeAddress.ToModel());
    }    

    public static IEnumerable<RouteAddressViewModel> ToViewModels(this IEnumerable<RouteAddressModel> models)
    {
        return models.Select(routeAddress => routeAddress.ToViewModel());
    }
}
