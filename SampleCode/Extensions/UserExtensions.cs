using SampleCode.DTO;
using SampleCode.Other;
using SampleCode.ViewModels.Data;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions;

public static class UserExtensions
{
    public static UserDTO ToDto(this UserViewModel user)
    {
        return new UserDTO(user.Id, user.Username);
    }

    public static UserDTO ToDto(this UserModel user)
    {
        return new UserDTO(user.Id, user.Username);
    }

    public static UserModel ToModel(this UserViewModel user)
    {
        return new UserModel()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }

    public static UserModel ToModel(this UserDTO user)
    {
        return new UserModel()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }

    public static UserViewModel ToViewModel(this UserDTO user)
    {
        return new UserViewModel()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }

    public static UserViewModel ToViewModel(this UserModel user)
    {
        return new UserViewModel()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }

    public static IEnumerable<UserDTO> ToDtos(this IEnumerable<UserViewModel> users)
    {
        return users.Select(user => user.ToDto());
    }

    public static IEnumerable<UserDTO> ToDtos(this IEnumerable<UserModel> users)
    {
        return users.Select(user => user.ToDto());
    }

    public static IEnumerable<UserModel> ToModels(this IEnumerable<UserViewModel> users)
    {
        return users.Select(user => user.ToModel());
    }

    public static IEnumerable<UserModel> ToModels(this IEnumerable<UserDTO> users)
    {
        return users.Select(user => user.ToModel());
    }


    public static IEnumerable<UserViewModel> ToViewModels(this IEnumerable<UserDTO> users)
    {
        return users.Select(users => users.ToViewModel());
    }

    public static IEnumerable<UserViewModel> ToViewModels(this IEnumerable<UserModel> users)
    {
        return users.Select(users => users.ToViewModel());
    }
}