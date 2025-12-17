using Models.Other;
using SampleCode.ViewModels.Data;
using System;

namespace SampleCode.Maps;

public class UserMap : DataEntityMap<UserViewModel, UserModel>
{
    public override UserViewModel MapFromModel(UserModel model, bool loadRelatedEntities)
    {
        return new UserViewModel(model.Id, model.Username);                        
    }

    public override UserModel MapFromViewModel(UserViewModel viewModel, bool loadRelatedEntities)
    {
        throw new NotImplementedException();
    }
}
