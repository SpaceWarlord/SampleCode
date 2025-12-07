using SampleCode.Other;
using SampleCode.ViewModels.Data;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions;

public static class UserExtensions
{
    public static UserModel ToModel(this UserViewModel user)
    {
        return new UserModel()
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

    public static IEnumerable<UserModel> ToModels(this IEnumerable<UserViewModel> users)
    {
        return users.Select(user => user.ToModel());
    }    

    public static IEnumerable<UserViewModel> ToViewModels(this IEnumerable<UserModel> users)
    {
        return users.Select(users => users.ToViewModel());
    }
}