using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.DTO.Navigation;
using SampleCode.Extensions.Navigation;
using SampleCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class RouteAddressService : IPageService<RouteAddressDTO>
{
    private SampleDbContext _db;
    public RouteAddressService(SampleDbContext db)
    {
        _db = db;
    }

    public async Task<ObservableCollection<RouteAddressDTO>> GetAll()
    {                       
        List<RouteAddressModel> a = await _db.RouteAddresses.Include(i => i.Route).Include(a => a.Address).ToListAsync();
        return new ObservableCollection<RouteAddressDTO>(a.ToDtos());                    
    }

    public async Task<int> AddUpdate(RouteAddressDTO dto)
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
            found.Route = dto.Route.ToModel();
            found.Address = dto.Address.ToModel();
            found.Order = dto.Order;
            await _db.SaveChangesAsync();
            return found.Id;
        }
    }
}