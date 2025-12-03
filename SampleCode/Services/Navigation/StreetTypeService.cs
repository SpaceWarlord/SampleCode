using Microsoft.EntityFrameworkCore;
using Models;
using SampleCode.DTO.Navigation;
using SampleCode.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public class StreetTypeService : IPageService<StreetTypeDTO>
{
    private SampleDbContext _db;
    public StreetTypeService(SampleDbContext db)
    {
        _db = db;
    }
    public Task<int> AddUpdate(StreetTypeDTO dto)
    {
        throw new System.NotImplementedException();
    }

    public async Task<ObservableCollection<StreetTypeDTO>> GetAll()
    {
        return new ObservableCollection<StreetTypeDTO>(await _db.StreetTypes.Select(c => new StreetTypeDTO(c.Id, c.Code, c.Name, c.Common)).ToListAsync());
    }
}