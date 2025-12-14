using Models;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data;

namespace SampleCode.Maps;

public abstract class DataEntityMap<ViewModel, Model> : IDataEntityMap<ViewModel, Model> where ViewModel : DataViewModel where Model : IModel
{
    public abstract DataViewModel MapFromModel(Model model, bool loadRelatedEntities);

    public abstract Model MapFromViewModel(ViewModel viewModel, bool loadRelatedEntities);        
}
