using Models;
using SampleCode.ViewModels.Data;

namespace SampleCode.Interfaces;
//https://www.youtube.com/watch?v=OulyOgmTxOQ
public interface IDataEntityMap<ViewModel, Model> 
{
    DataViewModel MapFromModel(Model model, bool loadRelatedEntities);
    Model MapFromViewModel(ViewModel viewModel, bool loadRelatedEntities);    
}
