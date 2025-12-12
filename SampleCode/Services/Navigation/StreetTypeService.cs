using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class StreetTypeService : IPageService<StreetTypeModel>
{
    private SampleDbContext _db;
    public StreetTypeService(SampleDbContext db)
    {
        _db = db;
    }
    public Task<int> AddUpdate(StreetTypeModel dto)
    {
        throw new System.NotImplementedException();
    }

    public async Task<ObservableCollection<StreetTypeModel>> GetAll()
    {
        //return new ObservableCollection<StreetTypeModel>(await _db.StreetTypes.Select(c => new StreetTypeModel(c.Id, c.Code, c.Name, c.Common)).ToListAsync());
        return null;
    }
}