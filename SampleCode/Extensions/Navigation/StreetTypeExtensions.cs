using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions.Navigation;

public static class StreetTypeExtensions
{
    

    public static StreetTypeModel ToModel(this StreetTypeViewModel streetType)
    {
        return new StreetTypeModel()
        {
            Id = streetType.Id,
            Code = streetType.Code,
            Name = streetType.Name,
            Common = streetType.Common
        };
    }    

    public static StreetTypeViewModel ToViewModel(this StreetTypeModel streetType)
    {
        return new StreetTypeViewModel(streetType.Id, streetType.Code, streetType.Name, streetType.Common);
    }    

    public static IEnumerable<StreetTypeModel> ToModels(this IEnumerable<StreetTypeViewModel> streetTypes)
    {
        return streetTypes.Select(streetType => streetType.ToModel());
    }    

    public static IEnumerable<StreetTypeViewModel> ToViewModels(this IEnumerable<StreetTypeModel> streetTypes)
    {
        return streetTypes.Select(streetType => streetType.ToViewModel());
    }
}