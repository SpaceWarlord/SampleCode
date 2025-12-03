using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class AddressExtensions
{
    public static AddressDTO ToDto(this AddressViewModel address)
    {
        if (address != null)
        {
            return new AddressDTO(address.Id, address.Name, address.UnitNum, address.StreetNum, address.StreetName, address.StreetType.ToDto(), address.Suburb.ToDto(), address.City, address.GPS);
        }
        return null;
    }

    public static AddressDTO ToDto(this AddressModel address)
    {
        if (address != null)
        {
            return new AddressDTO(address.Id, address.Name, address.UnitNum, address.StreetNum, address.StreetName, address.StreetType.ToDto(), address.Suburb.ToDto(), address.City, address.GPS);
        }
        return null;
    }

    public static AddressModel ToModel(this AddressViewModel address)
    {
        if (address != null)
        {
            return new AddressModel()
            {
                Id = address.Id,
                Name = address.Name,
                UnitNum = address.UnitNum,
                StreetNum = address.StreetNum,
                StreetName = address.StreetName,
                StreetType = address.StreetType.ToModel(),
                Suburb = address.Suburb.ToModel(),
                City = address.City,
            };
        }
        return null;
    }
    public static AddressModel ToModel(this AddressDTO address)
    {
        if (address != null)
        {
            return new AddressModel()
            {
                Id = address.Id,
                Name = address.Name,
                UnitNum = address.UnitNum,
                StreetNum = address.StreetNum,
                StreetName = address.StreetName,
                StreetType = address.StreetType.ToModel(),
                Suburb = address.Suburb.ToModel(),
                City = address.City,
            };
        }
        return null;
    }

    public static AddressViewModel ToViewModel(this AddressModel address)
    {
        if (address != null)
        {
            return new AddressViewModel(address.Id, address.Name, address.UnitNum, address.StreetNum, address.StreetName, address.StreetType.ToViewModel(), address.Suburb.ToViewModel(), address.City, address.GPS);
        }
        return null;
    }

    public static AddressViewModel ToViewModel(this AddressDTO address)
    {
        if (address != null)
        {
            return new AddressViewModel(address.Id, address.Name, address.UnitNum, address.StreetNum, address.StreetName, address.StreetType.ToViewModel(),
                address.Suburb.ToViewModel(), address.City, address.GPS);
        }
        return null;
    }

    public static IEnumerable<AddressDTO> ToDtos(this IEnumerable<AddressViewModel> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToDto());
        }
        return null;
    }

    public static IEnumerable<AddressDTO> ToDtos(this IEnumerable<AddressModel> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToDto());
        }
        return null;
    }

    public static IEnumerable<AddressModel> ToModels(this IEnumerable<AddressViewModel> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToModel());
        }
        return null;
    }

    public static IEnumerable<AddressModel> ToModels(this IEnumerable<AddressDTO> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToModel());
        }
        return null;
    }
    public static IEnumerable<AddressViewModel> ToViewModels(this IEnumerable<AddressDTO> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToViewModel());
        }
        return null;
    }

    public static IEnumerable<AddressViewModel> ToViewModels(this IEnumerable<AddressModel> addresses)
    {
        if (addresses != null)
        {
            return addresses.Select(address => address.ToViewModel());
        }
        return null;
    }
}
