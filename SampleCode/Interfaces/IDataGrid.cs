using Microsoft.UI.Xaml;
using SampleCode.ViewModels.Page;
using Syncfusion.UI.Xaml.DataGrid;

namespace SampleCode.Interfaces
{
    public interface IDataGrid<T> where T : PageViewModel
    {
        T PageViewModel { get; set; }

        void OnPageLoad(object sender, RoutedEventArgs e);
        void OnMainGridLoad(object sender, RoutedEventArgs e);
        void MainDataGrid_AddNewRowInitiating(object? sender, AddNewRowInitiatingEventArgs e);
        void MainDataGrid_RowValidating(object? sender, RowValidatingEventArgs e);
        public void MainDataGrid_RowValidated(object? sender, RowValidatedEventArgs e);
    }
}