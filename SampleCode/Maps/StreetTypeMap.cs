using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;

namespace SampleCode.Maps;

internal class StreetTypeMap : DataEntityMap<StreetTypeViewModel, StreetTypeModel>
{
    public override StreetTypeViewModel MapFromModel(StreetTypeModel model, bool loadRelatedEntities)
    {
        return new StreetTypeViewModel(model.Id, model.Code, model.Name, model.Common);
    }

    public override StreetTypeModel MapFromViewModel(StreetTypeViewModel viewModel, bool loadRelatedEntities)
    {
        throw new NotImplementedException();
    }
}
