using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services.Navigation;

public sealed class SuburbService(SampleDbContext db) : IPageService<SuburbModel>
{
    private SampleDbContext _db = db;   

    public Task<int> AddUpdate(SuburbModel model)
    {
        throw new System.NotImplementedException();
    }

    public async Task<ObservableCollection<SuburbModel>> GetAll()
    {
        //return new ObservableCollection<SuburbModel>(await _db.Suburbs.Select(c => new SuburbModel(c.Id, c.Name, c.PostCode)).ToListAsync());
        return null;
    }
}