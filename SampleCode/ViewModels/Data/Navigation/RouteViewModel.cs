using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Models.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class RouteViewModel : DataViewModel
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
        public RouteViewModel(int id, string name, ObservableCollection<RouteAddressViewModel> routeAddresses, float distance)
        {
            Id = id;
            Name = name;
            RouteAddresses = routeAddresses;
            Distance = distance;
        }

        public async Task Add()
        {
            var db = new SampleDbContext();
            var model = new RouteModel
            {
                Name = Name,
                RouteAddresses=null,
                Distance = Distance,
            };
            db.Routes.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Update()
        {
            if (Id!=0)
            {
                var db = new SampleDbContext();
                var model = new RouteModel
                {
                    Id= Id,
                    Name = Name,
                    RouteAddresses = null,
                    Distance = Distance,
                };
                db.Routes.Update(model);
                await db.SaveChangesAsync();               
            }
        }        
    }
}