using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;

namespace SampleCode.Maps;

public class AddressMap : DataEntityMap<AddressViewModel, AddressModel>
{
    public override AddressViewModel MapFromModel(AddressModel model, bool loadRelatedEntities)
    {
        StreetTypeMap streetTypeMap = new StreetTypeMap();
        StreetTypeViewModel streetTypeVM = new StreetTypeViewModel();
        if (model.StreetType != null && loadRelatedEntities)
        {
            streetTypeVM = streetTypeMap.MapFromModel(model.StreetType, false);
        }
        SuburbMap suburbMap = new SuburbMap();
        SuburbViewModel suburbVM = new SuburbViewModel();
        if (model.Suburb!=null && loadRelatedEntities)
        {
            suburbVM= suburbMap.MapFromModel(model.Suburb, false);
        }                      

        AddressViewModel addressVM = new AddressViewModel(model.Id, model.Name, model.UnitNum, model.StreetNum, model.StreetName, streetTypeVM, suburbVM, model.City, model.GPS);
        return addressVM;
    }

    public override AddressModel MapFromViewModel(AddressViewModel viewModel, bool loadRelatedEntities)
    {
        throw new NotImplementedException();
    }    
}
