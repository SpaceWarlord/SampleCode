using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class RouteExtensions
{
    public static RouteDTO ToDto(this RouteViewModel route)
    {
        return new RouteDTO(route.Id, route.Name, route.RouteAddresses == null ? null : route.RouteAddresses.ToDtos().ToList(), route.Distance);
    }

    public static RouteDTO ToDto(this RouteModel route)
    {
        return new RouteDTO(route.Id, route.Name, route.RouteAddresses == null ? null : route.RouteAddresses.ToDtos().ToList(), route.Distance);
    }

    public static RouteModel ToModel(this RouteViewModel route)
    {
        return new RouteModel()
        {
            Id = route.Id,
            Name = route.Name,
            RouteAddresses = route.RouteAddresses == null ? null : route.RouteAddresses.ToModels().ToList()
        };
    }

    public static RouteModel ToModel(this RouteDTO route)
    {
        return new RouteModel()
        {
            Id = route.Id,
            Name = route.Name,
            RouteAddresses = route.RouteAddresses == null ? null : route.RouteAddresses.ToModels().ToList(),
        };
    }

    public static RouteViewModel ToViewModel(this RouteDTO route)
    {
        IEnumerable<RouteAddressViewModel> routesAddresses = new List<RouteAddressViewModel>();
        if (route.RouteAddresses == null)
        {
            Debug.WriteLine("null");
        }
        else
        {
            Debug.WriteLine("NOT null");
            routesAddresses = route.RouteAddresses.ToViewModels();
        }
        return new RouteViewModel()
        {
            Id = route.Id,
            Name = route.Name,
            RouteAddresses = new ObservableCollection<RouteAddressViewModel>(routesAddresses)
            //RouteAddresses = new ObservableCollection<RouteAddressViewModel>(route.RouteAddresses == null ? null : route.RouteAddresses.ToViewModels()),
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

    public static IEnumerable<RouteDTO> ToDtos(this IEnumerable<RouteViewModel> route)
    {
        return route.Select(route => route.ToDto());
    }

    public static IEnumerable<RouteDTO> ToDtos(this IEnumerable<RouteModel> route)
    {
        return route.Select(route => route.ToDto());
    }

    public static IEnumerable<RouteModel> ToModels(this IEnumerable<RouteViewModel> routes)
    {
        return routes.Select(route => route.ToModel());
    }

    public static IEnumerable<RouteModel> ToModels(this IEnumerable<RouteDTO> routes)
    {
        return routes.Select(route => route.ToModel());
    }

    public static IEnumerable<RouteViewModel> ToViewModels(this IEnumerable<RouteDTO> routes)
    {
        return routes.Select(routes => routes.ToViewModel());
    }

    public static IEnumerable<RouteViewModel> ToViewModels(this IEnumerable<RouteModel> routes)
    {
        return routes.Select(routes => routes.ToViewModel());
    }
}