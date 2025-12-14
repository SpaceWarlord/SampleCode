using Models.Navigation;
using SampleCode.ViewModels.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleCode.Maps
{
    public class RouteMap : DataEntityMap<RouteViewModel, RouteModel>
    {
        //public HashSet<RouteAddressModel> nodes = new();
        public override RouteViewModel MapFromModel(RouteModel model, bool loadRelatedEntities)
        {
            RouteViewModel routeVM = new RouteViewModel(model.Id, model.Name, null, model.Distance);
            RouteAddressMap routeAddressMap = new RouteAddressMap();
            AddressMap addressMap = new AddressMap();
            ObservableCollection<RouteAddressViewModel> list = new ObservableCollection<RouteAddressViewModel>();
            if (loadRelatedEntities)
            {
                if (model.RouteAddresses != null)
                {
                    foreach (RouteAddressModel routeAdressModel in model.RouteAddresses)
                    {
                        RouteAddressViewModel routeAddressViewModel = routeAddressMap.MapFromModel(routeAdressModel, false);
                        AddressViewModel addressViewModel = addressMap.MapFromModel(routeAdressModel.Address, false);
                        routeAddressViewModel.Address=addressViewModel;
                        list.Add(routeAddressViewModel);                        
                    }
                    routeVM.RouteAddresses = list;
                }
            }            
            //nodes.Clear();
            return routeVM;
        }

        public override RouteModel MapFromViewModel(RouteViewModel viewModel, bool loadRelatedEntities)
        {
            throw new NotImplementedException();
        }
    }
}
