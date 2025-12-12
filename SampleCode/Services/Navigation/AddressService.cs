using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class AddressService : IPageService<AddressModel>
{
    private SampleDbContext _db;
    public AddressService(SampleDbContext db)
    {
        _db = db;
    }
    public async Task<ObservableCollection<AddressModel>> GetAll()
    {
        //return new ObservableCollection<AddressModel>(await _db.Addresses.Select(c => new AddressModel(c.Id, c.Name, c.UnitNum, c.StreetNum, c.StreetName, c.StreetType.ToDto(), c.Suburb.ToDto(), c.City, c.GPS)).ToListAsync());                       
        return null;
    }

    public async Task<int> AddUpdate(AddressModel address)
    {
        Debug.WriteLine("-- AddUpdate --");
        var found = await _db.Addresses.FirstOrDefaultAsync(x => x.Id == address.Id);
        if (found is null)
        {
            var nameExists = await _db.Addresses.FirstOrDefaultAsync(x => x.Name == address.Name);
            if (nameExists is null)
            {
                bool pass = true;
                if (address.StreetType == null)
                {
                    Debug.WriteLine("Error: Street Type was null");
                    pass = false;
                }
                if (address.Suburb == null)
                {
                    Debug.WriteLine("Error: Suburb was null");
                    pass = false;
                }
                if (pass)
                {                        
                    var a = new AddressModel()
                    {
                        Name = address.Name,
                        UnitNum = address.UnitNum,
                        StreetNum = address.StreetNum,
                        StreetName = address.StreetName,
                        StreetTypeId = address.StreetType.Id,
                        SuburbId = address.Suburb.Id,

                    };
                    _db.Addresses.Add(a);
                    await _db.SaveChangesAsync();
                    return a.Id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            var nameExists = await _db.Addresses.FirstOrDefaultAsync(x => x.Name == address.Name && x.Id != address.Id);
            if (nameExists is null)
            {
                found.Name = address.Name;
                found.UnitNum = address.UnitNum;
                found.StreetNum = address.StreetNum;
                found.StreetName = address.StreetName;
               // found.StreetType = address.StreetType.ToModel();
               // found.Suburb = address.Suburb.ToModel();
                found.City = address.City;
                await _db.SaveChangesAsync();
                return found.Id;
            }
            else
            {
                return 0;
            }
        }
    }    
}