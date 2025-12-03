using SampleCode.DTO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleCode.Interfaces
{
    public interface IPageService<T> where T : BaseDTO
    {
        Task<ObservableCollection<T>> GetAll();
        Task<int> AddUpdate(T dto);
    }
}
