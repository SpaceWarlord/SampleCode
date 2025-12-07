using SampleCode.ViewModels.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleCode.Interfaces;

public interface IPageViewModel<T> where T : DataViewModel
{
    ObservableCollection<T> PageItemsList { get; set; }
    Task LoadData();
    Task Add(T viewModel);
    Task Update(T viewModel);

    Task Delete(T viewModel);
}