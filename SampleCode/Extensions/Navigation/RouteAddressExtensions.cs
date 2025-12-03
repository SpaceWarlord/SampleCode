using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class RouteAddressExtensions
{
    public static RouteAddressDTO ToDto(this RouteAddressViewModel vm)
    {
        return new RouteAddressDTO(vm.Id, vm.Route.ToDto(), vm.Address.ToDto(), vm.Order);
    }

    public static RouteAddressDTO ToDto(this RouteAddressModel model)
    {
        if (model.Route == null)
        {
            Debug.WriteLine("Route address route was null");
            return null;
        }
        else
        {
            return new RouteAddressDTO(model.Id, model.Route.ToDto(), model.Address.ToDto(), model.Order);
        }
    }

    public static RouteAddressModel ToModel(this RouteAddressViewModel vm)
    {
        return new RouteAddressModel()
        {
            Route = vm.Route.ToModel(),
            Address = vm.Address.ToModel(),
            Order = vm.Order
        };
    }
    public static RouteAddressModel ToModel(this RouteAddressDTO dto)
    {
        return new RouteAddressModel()
        {
            Route = dto.Route.ToModel(),
            Address = dto.Address.ToModel(),
            Order = dto.Order
        };
    }

    public static RouteAddressViewModel ToViewModel(this RouteAddressModel model)
    {
        return new RouteAddressViewModel(model.Route.ToViewModel(), model.Address.ToViewModel(), model.Order);
    }

    public static RouteAddressViewModel ToViewModel(this RouteAddressDTO dto)
    {
        return new RouteAddressViewModel(dto.Route.ToViewModel(), dto.Address.ToViewModel(), dto.Order);
    }

    public static IEnumerable<RouteAddressDTO> ToDtos(this IEnumerable<RouteAddressViewModel> vms)
    {
        return vms.Select(routeAddress => routeAddress.ToDto());
    }

    public static IEnumerable<RouteAddressDTO> ToDtos(this IEnumerable<RouteAddressModel> vms)
    {
        Debug.WriteLine("Test");
        return vms.Select(routeAddress => routeAddress.ToDto());
    }

    public static IEnumerable<RouteAddressModel> ToModels(this IEnumerable<RouteAddressViewModel> vms)
    {
        return vms.Select(routeAddress => routeAddress.ToModel());
    }

    public static IEnumerable<RouteAddressModel> ToModels(this IEnumerable<RouteAddressDTO> dtos)
    {
        return dtos.Select(routeAddress => routeAddress.ToModel());
    }
    public static IEnumerable<RouteAddressViewModel> ToViewModels(this IEnumerable<RouteAddressDTO> dtos)
    {
        return dtos.Select(routeAddress => routeAddress == null ? null : routeAddress.ToViewModel());
    }

    public static IEnumerable<RouteAddressViewModel> ToViewModels(this IEnumerable<RouteAddressModel> models)
    {
        return models.Select(routeAddress => routeAddress.ToViewModel());
    }
}
