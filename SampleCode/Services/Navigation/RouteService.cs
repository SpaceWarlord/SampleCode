using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class RouteService : IPageService<RouteModel>
{
    private SampleDbContext _db;
    public RouteService(SampleDbContext db)
    {
        _db = db;
    }

    public async Task<ObservableCollection<RouteModel>> GetAll()
    {            
        ObservableCollection<RouteModel> bb = new ObservableCollection<RouteModel>();
        List<RouteModel> a = await _db.Routes.Include(i => i.RouteAddresses).ToListAsync();
        foreach (RouteModel model in a)
        {
            //bb.Add(model.ToDto());
        }
        return bb;                                   
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
}