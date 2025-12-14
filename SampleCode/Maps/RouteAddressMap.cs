using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SampleCode.Maps;

public class RouteAddressMap : DataEntityMap<RouteAddressViewModel, RouteAddressModel>
{
    //public HashSet<RouteAddressModel> nodes = new();
    public override RouteAddressViewModel MapFromModel(RouteAddressModel model, bool loadRelatedEntities)
    {
        RouteMap routeMap = new RouteMap();
        RouteViewModel routeVM = new RouteViewModel();
        AddressMap addressMap = new AddressMap();
        AddressViewModel addressVM = new AddressViewModel();
        if (loadRelatedEntities)
        {
            Debug.WriteLine("LOAD RELATED");
            if (model.Route != null)
            {
                routeVM = routeMap.MapFromModel(model.Route, false);
            }
            if (model.Address != null)
            {
                addressVM = addressMap.MapFromModel(model.Address, true);
            }
        }
                
        /*
        if (model.Route.RouteAddresses!=null)
        {
            List<RouteAddressViewModel> list = new List<RouteAddressViewModel>();
            foreach (RouteAddressModel routeAddressModel in model.Route.RouteAddresses)
            {
                list.Add(a.MapFromModel(routeAddressModel));
            }
        }
       */
        //RouteViewModel routeViewModel = new RouteViewModel(model.Route.Id, model.Route.Name, model.Route);

        
               
        RouteAddressViewModel routeAddressVM = new RouteAddressViewModel();
        routeAddressVM = new RouteAddressViewModel(routeVM, addressVM, model.Order);
        /*
        if (nodes.Add(model))
        {
            routeAddressVM = new RouteAddressViewModel(routeVM, addressVM, model.Order);
        } 
        */
        return routeAddressVM;
    }

    public override RouteAddressModel MapFromViewModel(RouteAddressViewModel viewModel, bool loadRelatedEntities)
    {
        throw new NotImplementedException();
    }
}
