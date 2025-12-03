using Microsoft.EntityFrameworkCore;
using Models;
using SampleCode.DTO.Navigation;
using SampleCode.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation
{
    public class SuburbService : BaseService, IPageService<SuburbDTO>
    {
        public SuburbService(SampleDbContext db)
        {
            _db = db;
        }

        public Task<int> AddUpdate(SuburbDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ObservableCollection<SuburbDTO>> GetAll()
        {
            return new ObservableCollection<SuburbDTO>(await _db.Suburbs.Select(c => new SuburbDTO(c.Id, c.Name, c.PostCode)).ToListAsync());
        }
    }
}