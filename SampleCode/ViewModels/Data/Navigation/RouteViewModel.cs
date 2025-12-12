using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class RouteViewModel : DataViewModel, IViewModel<RouteViewModel>
    {
        [ObservableProperty]
        [Required]
        [NotNull]
        private string _name;

        [ObservableProperty]
        [Required]
        [NotNull]
        private ObservableCollection<RouteAddressViewModel>? _routeAddresses;


        [ObservableProperty]
        private float _distance;

        public RouteViewModel()
        {

        }
        public RouteViewModel(int id, string name, List<RouteAddressViewModel> routeAddresses, float distance)
        {
            Id = id;
            Name = name;
            RouteAddresses = new ObservableCollection<RouteAddressViewModel>(routeAddresses);
            Distance = distance;
        }

        public RouteViewModel(RouteModel model)
        {
            Id= model.Id;
            Name = model.Name;
            RouteAddresses = new ObservableCollection<RouteAddressViewModel>(RouteAddressViewModel.ToViewModels(model.RouteAddresses));
            Distance = model.Distance;
        }

        public static IQueryable<RouteModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<RouteModel> routes1 = db.Routes.Include(m => m.RouteAddresses);
            IQueryable<RouteAddressModel> routes2 = db.RouteAddresses;
            Debug.WriteLine("hi");
            /*
            IQueryable<RouteModel> routes = db.Routes.Select(c => new RouteViewModel(c.Id, c.Name, new ObservableCollection<RouteAddressViewModel>(c.RouteAddresses.ToViewModels()), c.Distance));
            IQueryable<RouteAddressModel> query1 = RouteAddressViewModel.GetAll1();
            foreach (RouteAddressViewModel routeAddress in query1)
            {

            }
            */
            //IQueryable<RouteViewModel> query = db.Routes.Select(c => new RouteViewModel(c.Id, c.Name, new ObservableCollection<RouteAddressViewModel>(c.RouteAddresses.ToViewModels()), c.Distance));
            return routes1;            
        }        

        public async Task Add()
        {
            using (var db = new SampleDbContext())
            {
                var model = new RouteModel
                {
                    Name = Name,
                    RouteAddresses = null,
                    Distance = Distance,
                };
                db.Routes.Add(model);
                await db.SaveChangesAsync();
            }                
        }

        public Task Delete()
        {
            throw new System.NotImplementedException();
        }

        public async Task Update()
        {
            if (Id!=0)
            {
                var db = new SampleDbContext();
                //List<RouteAddressModel>? routeAddresses=db.RouteAddresses.Select(a => a.Id==Id);
                IQueryable<RouteAddressModel> aa = RouteAddressViewModel.GetAll();

                aa.Select(a => a.Route.Id == Id).ToList();

                IQueryable<RouteAddressModel> allAddresses = RouteAddressViewModel.GetAll1();
                var relatedAddresses = allAddresses.Where(a => a.Route.Id == Id).ToList();
                //var a =RouteAddressViewModel.GetAll().Select(a => a.Route.Id == Id).ToList() ;
                //var routeAddresses = db.RouteAddresses.Select(a => a.Route.Id == Id).ToList();
                var model = new RouteModel
                {
                    Id= Id,
                    Name = Name,
                    RouteAddresses = relatedAddresses,
                    Distance = Distance,
                };
                db.Routes.Update(model);
                await db.SaveChangesAsync();               
            }
        }  
        
        public static List<RouteViewModel> ToViewModels(List<RouteModel> models)
        {
            List<RouteViewModel> list = new List<RouteViewModel>();
            foreach (RouteModel model in models)
            {
                list.Add(new RouteViewModel(model.Id, model.Name, RouteAddressViewModel.ToViewModels(model.RouteAddresses), model.Distance));
            }
            return list;
        }
    }
}