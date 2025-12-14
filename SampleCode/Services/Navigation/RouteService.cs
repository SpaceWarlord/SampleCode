using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleCode.Services.Navigation;

public sealed class RouteService(SampleDbContext db) : IPageService<RouteModel>
{
    private SampleDbContext _db = db;
    

    /*
    public async Task<ObservableCollection<RouteModel>> GetAll()
    {
        using (var db = new SampleDbContext())
        {
            ObservableCollection<RouteModel> bb = new ObservableCollection<RouteModel>();
            List<RouteModel> a = await _db.Routes.Include(i => i.RouteAddresses).ToListAsync();
            foreach (RouteModel model in a)
            {
                //bb.Add(model.ToDto());
            }
            return bb;
        }
    }
    */

    public async Task<IEnumerable<RouteModel>> GetAll()
    {
        IQueryable<RouteModel> routes1 = db.Routes.Include(m => m.RouteAddresses).ThenInclude(m => m.Address);
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

        return await routes1.ToListAsync();

    }

    public async Task Add(RouteViewModel vm)
    {
        var model = new RouteModel
        {
            Name = vm.Name,
            RouteAddresses = null,
            Distance = vm.Distance,
        };
        db.Routes.Add(model);
        await db.SaveChangesAsync();
    }

    public Task Delete()
    {
        throw new System.NotImplementedException();
    }

    public async Task Update(RouteViewModel vm)
    {
        if (vm.Id != 0)
        {
            var db = new SampleDbContext();
            //List<RouteAddressModel>? routeAddresses=db.RouteAddresses.Select(a => a.Id==Id);
            IQueryable<RouteAddressModel> aa = RouteAddressViewModel.GetAll();

            aa.Select(a => a.Route.Id == vm.Id).ToList();

            IQueryable<RouteAddressModel> allAddresses = RouteAddressViewModel.GetAll1();
            var relatedAddresses = allAddresses.Where(a => a.Route.Id == vm.Id).ToList();
            //var a =RouteAddressViewModel.GetAll().Select(a => a.Route.Id == Id).ToList() ;
            //var routeAddresses = db.RouteAddresses.Select(a => a.Route.Id == Id).ToList();
            var model = new RouteModel
            {
                Id = vm.Id,
                Name = vm.Name,
                RouteAddresses = relatedAddresses,
                Distance = vm.Distance,
            };
            db.Routes.Update(model);
            await db.SaveChangesAsync();
        }
    }


    public async Task<int> AddUpdate(RouteModel model)
    {
        Debug.WriteLine("-- AddUpdate --");
        var found = await _db.Routes.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (found is null)
        {

            var i = new RouteModel()
            {
                Name = model.Name,
                //RouteAddresses = model.RouteAddresses == null ? null : model.RouteAddresses.ToModels().ToList(),
                Distance = model.Distance,
            };
            _db.Routes.Add(i);
            await _db.SaveChangesAsync();
            return i.Id;
        }
        else
        {
            found.Name = model.Name;
            //found.RouteAddresses = model.RouteAddresses == null ? null : model.RouteAddresses.ToModels().ToList();
            found.Distance = model.Distance;
            await _db.SaveChangesAsync();
            return found.Id;
        }
    }

    Task<ObservableCollection<RouteModel>> IPageService<RouteModel>.GetAll()
    {
        throw new System.NotImplementedException();
    }
}