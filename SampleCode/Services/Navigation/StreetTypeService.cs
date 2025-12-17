using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public sealed class StreetTypeService(SampleDbContext db) : IPageService<StreetTypeModel>
{
    private SampleDbContext _db = db;
    
    public Task<int> AddUpdate(StreetTypeModel dto)
    {
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<StreetTypeModel>> GetAll()
    {
        //return new ObservableCollection<StreetTypeModel>(await _db.StreetTypes.Select(c => new StreetTypeModel(c.Id, c.Code, c.Name, c.Common)).ToListAsync());
        return null;
    }    
}