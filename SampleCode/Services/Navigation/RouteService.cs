using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.Extensions.Navigation;
using SampleCode.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class RouteService : IPageService<RouteDTO>
{
    private SampleDbContext _db;
    public RouteService(SampleDbContext db)
    {
        _db = db;
    }

    public async Task<ObservableCollection<RouteDTO>> GetAll()
    {            
        ObservableCollection<RouteDTO> bb = new ObservableCollection<RouteDTO>();
        List<RouteModel> a = await _db.Routes.Include(i => i.RouteAddresses).ToListAsync();
        foreach (RouteModel model in a)
        {
            bb.Add(model.ToDto());
        }
        return bb;                                   
    }

    public async Task<int> AddUpdate(RouteDTO dto)
    {
        Debug.WriteLine("-- AddUpdate --");
        var found = await _db.Routes.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (found is null)
        {

            var i = new RouteModel()
            {
                Name = dto.Name,
                RouteAddresses = dto.RouteAddresses == null ? null : dto.RouteAddresses.ToModels().ToList(),
                Distance = dto.Distance,
            };
            _db.Routes.Add(i);
            await _db.SaveChangesAsync();
            return i.Id;
        }
        else
        {
            found.Name = dto.Name;
            found.RouteAddresses = dto.RouteAddresses == null ? null : dto.RouteAddresses.ToModels().ToList();
            found.Distance = dto.Distance;
            await _db.SaveChangesAsync();
            return found.Id;
        }
    }
}