using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public sealed class RouteAddressService(SampleDbContext db) : IPageService<RouteAddressModel>
{
    private SampleDbContext _db = db;   
    public async Task<ObservableCollection<RouteAddressModel>> GetAll()
    {                       
        List<RouteAddressModel> a = await _db.RouteAddresses.Include(i => i.Route).Include(a => a.Address).ToListAsync();
        //return new ObservableCollection<RouteAddressModel>(a.ToDtos());                    
        return null;
    }

    public async Task<int> AddUpdate(RouteAddressModel dto)
    {
        Debug.WriteLine("-- AddUpdate --");
        var found = await _db.RouteAddresses.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (found is null)
        {
            var i = new RouteAddressModel()
            {
                RouteId = dto.Route.Id,
                AddressId = dto.Address.Id,                   
                Order = dto.Order,
            };
            _db.RouteAddresses.Add(i);
            await _db.SaveChangesAsync();
            return i.Id;
        }
        else
        {
            //found.Route = dto.Route.ToModel();
            //found.Address = dto.Address.ToModel();
            found.Order = dto.Order;
            await _db.SaveChangesAsync();
            return found.Id;
        }
    }

    Task<IEnumerable<RouteAddressModel>> IPageService<RouteAddressModel>.GetAll()
    {
        throw new NotImplementedException();
    }
}