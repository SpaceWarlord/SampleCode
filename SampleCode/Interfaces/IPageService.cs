using Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleCode.Interfaces;

public interface IPageService<T> where T : IModel
{
    Task<IEnumerable<T>> GetAll();
    Task<int> AddUpdate(T model);
}
