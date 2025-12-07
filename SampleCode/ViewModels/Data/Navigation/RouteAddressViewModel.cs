using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Models.Navigation;
using SampleCode.Extensions.Navigation;
using SampleCode.Interfaces;
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

        public static IQueryable<RouteAddressViewModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<RouteAddressViewModel> query = db.RouteAddresses.Select(c => new RouteAddressViewModel(
                new RouteViewModel(c.Route.Id, c.Route.Name, new ObservableCollection<RouteAddressViewModel>(c.Route.RouteAddresses.ToViewModels()), c.Route.Distance), 
                new AddressViewModel(c.Address.Id, c.Address.Name, c.Address.UnitNum, c.Address.StreetNum, c.Address.StreetName, 
                new StreetTypeViewModel(c.Address.StreetType.Id, c.Address.StreetType.Code, c.Address.StreetType.Name, c.Address.StreetType.Common), 
                new SuburbViewModel(c.Address.Suburb.Id, c.Address.Suburb.Name, c.Address.Suburb.PostCode), c.Address.City, c.Address.GPS), 
                c.Order));
            return query;
        }

        public async Task Add()
        {
            var db = new SampleDbContext();            
            var model = new RouteAddressModel()
            {
                RouteId = Route.Id,
                AddressId = Address.Id,
                Order = Order

            };
            db.RouteAddresses.Add(model);
            await db.SaveChangesAsync();            
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
    }
}