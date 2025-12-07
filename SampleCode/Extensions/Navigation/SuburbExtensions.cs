using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class SuburbExtensions
{
    public static SuburbModel ToModel(this SuburbViewModel suburb)
    {
        return new SuburbModel()
        {
            Id = suburb.Id,
            Name = suburb.Name,
            PostCode = suburb.PostCode,
        };
    }    

    public static SuburbViewModel ToViewModel(this SuburbModel suburb)
    {
        return new SuburbViewModel(suburb.Id, suburb.Name, suburb.PostCode);
    }
   
    public static IEnumerable<SuburbModel> ToModels(this IEnumerable<SuburbViewModel> suburbs)
    {
        return suburbs.Select(suburb => suburb.ToModel());
    }   

    public static IEnumerable<SuburbViewModel> ToViewModels(this IEnumerable<SuburbModel> suburbs)
    {
        return suburbs.Select(suburb => suburb.ToViewModel());
    }
}