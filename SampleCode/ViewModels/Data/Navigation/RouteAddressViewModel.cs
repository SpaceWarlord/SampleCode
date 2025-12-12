using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class RouteAddressViewModel : DataViewModel, IViewModel<RouteAddressViewModel>
    {
        [ObservableProperty]
        private RouteViewModel _route;

        [ObservableProperty]
        private AddressViewModel _address;

        [ObservableProperty]
        private int _order;

        public RouteAddressViewModel()
        {

        }

        public RouteAddressViewModel(RouteViewModel route, AddressViewModel address, int order)
        {
            Route = route;
            Address = address;
            Order = order;
        }

        public RouteAddressViewModel(RouteAddressModel model)
        {
            Route = new RouteViewModel(model.Route);
            Address = new AddressViewModel(model.Address);
            Order = model.Order;
        }

        public static IQueryable<RouteAddressModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<RouteAddressModel> query = db.RouteAddresses.Include(m=> m.Route);
            /*
            IQueryable<RouteAddressViewModel> query = db.RouteAddresses.Select(c => new RouteAddressViewModel(
                new RouteViewModel(c.Route.Id, c.Route.Name, new ObservableCollection<RouteAddressViewModel>(c.Route.RouteAddresses.ToViewModels()), c.Route.Distance), 
                new AddressViewModel(c.Address.Id, c.Address.Name, c.Address.UnitNum, c.Address.StreetNum, c.Address.StreetName, 
                new StreetTypeViewModel(c.Address.StreetType.Id, c.Address.StreetType.Code, c.Address.StreetType.Name, c.Address.StreetType.Common), 
                new SuburbViewModel(c.Address.Suburb.Id, c.Address.Suburb.Name, c.Address.Suburb.PostCode), c.Address.City, c.Address.GPS), 
                c.Order));
            */
            return query;
        }
        
        public static IQueryable<RouteAddressModel> GetAll1()
        {
            var db = new SampleDbContext();
            IQueryable<RouteAddressModel> query = db.RouteAddresses.Select(c => new RouteAddressModel
            {
                Id = c.Id,
                RouteId = c.RouteId,
                AddressId = c.AddressId,
                Order = c.Order
            });               
            return query;
        }

        public static IQueryable<RouteAddressModel> GetRelated(int routeId)
        {
            var db = new SampleDbContext();
            IQueryable<RouteAddressModel> query = RouteAddressViewModel.GetAll1();
            IQueryable<RouteAddressModel> related = query.Where(c => c.RouteId == routeId);
            return related;
        }
        
        public async Task Add()
        {
            Debug.WriteLine("-- Add Route Address --");
            using (var db = new SampleDbContext())
            {
                var route = db.Routes.SingleOrDefault(p => p.Id == Route.Id);
                if (route!=null)
                {
                    var address=db.Addresses.SingleOrDefault(a => a.Id == Address.Id);
                    if (address!=null)
                    {
                        var model = new RouteAddressModel()
                        {
                            Route = route,
                            Address = address,
                            Order = Order

                        };
                        db.RouteAddresses.Add(model);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        Debug.WriteLine("Address not found");
                    }
                }
                else
                {
                    Debug.WriteLine("route not found");
                }
            }                          
        }        

        public async Task Delete()
        {
            var db = new SampleDbContext();
            db.Remove(Id);
            await db.SaveChangesAsync();
        }

        public async Task Update()
        {
            if (Id != 0)
            {
                var db = new SampleDbContext();
                var model = new RouteAddressModel()
                {
                    Id = Id,
                    RouteId = Route.Id,
                    AddressId = Address.Id,
                    Order = Order
                };
                db.RouteAddresses.Update(model);
                await db.SaveChangesAsync();
            }
        }

        public static List<RouteAddressViewModel> ToViewModels(List<RouteAddressModel>? models)
        {
            List<RouteAddressViewModel> list = new List<RouteAddressViewModel>();
            if (models!=null)
            {
                foreach (RouteAddressModel model in models)
                {
                    list.Add(new RouteAddressViewModel(new RouteViewModel(model.Route.Id, model.Route.Name, RouteAddressViewModel.ToViewModels(model.Route.RouteAddresses), model.Route.Distance), 
                        new AddressViewModel(model.Address), model.Order));
                }
            }            
            return list;
        }
    }
}