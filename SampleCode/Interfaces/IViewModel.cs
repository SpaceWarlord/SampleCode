using SampleCode.ViewModels.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Interfaces;

public interface IViewModel<T> where T: DataViewModel
{
    public Task Add();
    public Task Update();
    public Task Delete();
    public static abstract IQueryable<T> GetAll();    
}
