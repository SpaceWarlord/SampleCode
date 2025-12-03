using SampleCode.ViewModels.Page;
using Syncfusion.UI.Xaml.DataGrid;

namespace SampleCode.Interfaces;

public interface ISubDataGrid<T, S> : IDataGrid<T> where T : PageViewModel where S : PageViewModel
{
    S SubPageViewModel { get; set; }

    void SubDataGrid_AddNewRowInitiating(object? sender, AddNewRowInitiatingEventArgs e);
    void SubDataGridDataGrid_RowValidating(object? sender, RowValidatingEventArgs e);
    void SubDataGrid_RowValidated(object? sender, RowValidatedEventArgs e);
}
