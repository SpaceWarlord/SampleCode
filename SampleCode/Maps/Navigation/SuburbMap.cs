using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;

namespace SampleCode.Maps.Navigation;

public class SuburbMap : DataEntityMap<SuburbViewModel, SuburbModel>
{
    public override SuburbViewModel MapFromModel(SuburbModel model, bool loadRelatedEntities)
    {
        return new SuburbViewModel(model.Id, model.Name, model.PostCode);
    }

    public override SuburbModel MapFromViewModel(SuburbViewModel viewModel, bool loadRelatedEntities)
    {
        throw new NotImplementedException();
    }
}
