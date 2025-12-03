using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Extensions.Navigation
{
    public static class SuburbExtensions
    {
        public static SuburbDTO ToDto(this SuburbViewModel suburb)
        {
            return new SuburbDTO(suburb.Id, suburb.Name, suburb.PostCode);
        }

        public static SuburbDTO ToDto(this SuburbModel suburb)
        {
            return new SuburbDTO(suburb.Id, suburb.Name, suburb.PostCode);
        }

        public static SuburbModel ToModel(this SuburbViewModel suburb)
        {
            return new SuburbModel()
            {
                Id = suburb.Id,
                Name = suburb.Name,
                PostCode = suburb.PostCode,
            };
        }
        public static SuburbModel ToModel(this SuburbDTO suburb)
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

        public static SuburbViewModel ToViewModel(this SuburbDTO suburb)
        {
            return new SuburbViewModel(suburb.Id, suburb.Name, suburb.PostCode);
        }

        public static IEnumerable<SuburbDTO> ToDtos(this IEnumerable<SuburbViewModel> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToDto());
        }

        public static IEnumerable<SuburbDTO> ToDtos(this IEnumerable<SuburbModel> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToDto());
        }

        public static IEnumerable<SuburbModel> ToModels(this IEnumerable<SuburbViewModel> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToModel());
        }

        public static IEnumerable<SuburbModel> ToModels(this IEnumerable<SuburbDTO> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToModel());
        }

        public static IEnumerable<SuburbViewModel> ToViewModels(this IEnumerable<SuburbDTO> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToViewModel());
        }

        public static IEnumerable<SuburbViewModel> ToViewModels(this IEnumerable<SuburbModel> suburbs)
        {
            return suburbs.Select(suburb => suburb.ToViewModel());
        }
    }
}