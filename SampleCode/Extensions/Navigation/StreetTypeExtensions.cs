using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SampleCode.Extensions.Navigation
{
    public static class StreetTypeExtensions
    {
        public static StreetTypeDTO ToDto(this StreetTypeViewModel streetType)
        {
            return new StreetTypeDTO(streetType.Id, streetType.Code, streetType.Name, streetType.Common);
        }

        public static StreetTypeDTO ToDto(this StreetTypeModel streetType)
        {            
            return new StreetTypeDTO(streetType.Id, streetType.Code, streetType.Name, streetType.Common);
        }

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
        public static StreetTypeModel ToModel(this StreetTypeDTO streetType)
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

        public static StreetTypeViewModel ToViewModel(this StreetTypeDTO streetType)
        {
            return new StreetTypeViewModel(streetType.Id, streetType.Code, streetType.Name, streetType.Common);
        }

        public static IEnumerable<StreetTypeDTO> ToDtos(this IEnumerable<StreetTypeViewModel> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToDto());
        }

        public static IEnumerable<StreetTypeDTO> ToDtos(this IEnumerable<StreetTypeModel> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToDto());
        }

        public static IEnumerable<StreetTypeModel> ToModels(this IEnumerable<StreetTypeViewModel> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToModel());
        }

        public static IEnumerable<StreetTypeModel> ToModels(this IEnumerable<StreetTypeDTO> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToModel());
        }

        public static IEnumerable<StreetTypeViewModel> ToViewModels(this IEnumerable<StreetTypeDTO> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToViewModel());
        }

        public static IEnumerable<StreetTypeViewModel> ToViewModels(this IEnumerable<StreetTypeModel> streetTypes)
        {
            return streetTypes.Select(streetType => streetType.ToViewModel());
        }
    }
}