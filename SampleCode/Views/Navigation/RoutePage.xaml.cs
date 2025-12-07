using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using SampleCode.ViewModels.Page.Navigation;
using Syncfusion.UI.Xaml.DataGrid;
using System.Diagnostics;

namespace SampleCode.Views.Navigation;

public sealed partial class RoutePage : Page, ISubDataGrid<RoutePageViewModel, RouteAddressPageViewModel>
{
    public RoutePageViewModel PageViewModel { get; set; }
    public RouteAddressPageViewModel SubPageViewModel { get; set; }

    public RoutePage()
    {
        this.InitializeComponent();
        PageViewModel = new RoutePageViewModel();
        SubPageViewModel = new RouteAddressPageViewModel();
        DataContext = PageViewModel;
        MainDataGrid.AddNewRowInitiating += MainDataGrid_AddNewRowInitiating;
        MainDataGrid.DataValidationMode = Syncfusion.UI.Xaml.Grids.GridValidationMode.InView;
        MainDataGrid.RowValidated += MainDataGrid_RowValidated;
        MainDataGrid.RowValidating += MainDataGrid_RowValidating;
        RouteAddressGrid.AddNewRowInitiating += SubDataGrid_AddNewRowInitiating;
        RouteAddressGrid.DataValidationMode = Syncfusion.UI.Xaml.Grids.GridValidationMode.InView;
        RouteAddressGrid.RowValidated += SubDataGrid_RowValidated;
        RouteAddressGrid.RowValidating += SubDataGridDataGrid_RowValidating;
    }

    public void MainDataGrid_AddNewRowInitiating(object? sender, AddNewRowInitiatingEventArgs e)
    {
        Debug.WriteLine("add new row");
    }

    public async void MainDataGrid_RowValidated(object? sender, RowValidatedEventArgs e)
    {
        Debug.WriteLine("row validated");
        RouteViewModel? route = e.RowData as RouteViewModel;
        if (route != null)
        {
            Debug.WriteLine("It's validated");
            await PageViewModel.Add(route);
        }
        else
        {
            Debug.WriteLine("It's not validated");
        }
    }

    public void MainDataGrid_RowValidating(object? sender, RowValidatingEventArgs e)
    {
        Debug.WriteLine("validating");
        RouteViewModel? route = e.RowData as RouteViewModel;
        if (route != null)
        {
            if (string.IsNullOrWhiteSpace(route.Name))
            {
                Debug.WriteLine("error name is blank");
                e.IsValid = false;
                e.ErrorMessages.Add("Name", "Error: Name cannot be blank");
            }
            else
            {
                Debug.WriteLine("its valid");
            }
        }
        else
        {
            Debug.WriteLine("route is null");
        }
    }

    public void OnMainGridLoad(object sender, RoutedEventArgs e)
    {

    }

    public async void OnPageLoad(object sender, RoutedEventArgs e)
    {
        await PageViewModel.LoadData();
        await PageViewModel.SubPageViewModel.LoadData();
        MainDataGrid.ItemsSource = PageViewModel.PageItemsList;
    }

    public void SubDataGrid_AddNewRowInitiating(object? sender, AddNewRowInitiatingEventArgs e)
    {
        if (e.OriginalSender is DetailsViewDataGrid detailsGrid)
        {
            var parentDataGrid = detailsGrid.NotifyListener?.GetParentDataGrid();
            if (parentDataGrid != null)
            {
                var currentItem = parentDataGrid.CurrentItem;
                if (currentItem != null && currentItem is RouteViewModel)
                {
                    RouteViewModel? currentDataRow = parentDataGrid.CurrentItem as RouteViewModel;
                    if (e.NewObject != null && e.NewObject is RouteAddressViewModel)
                    {
                        RouteAddressViewModel? routeAddress = e.NewObject as RouteAddressViewModel;
                        if (routeAddress != null)
                        {
                            if (currentDataRow != null)
                            {
                                Debug.WriteLine("ROUTE ID: " + currentDataRow.Id);
                                routeAddress.Route = currentDataRow;
                            }
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    public void SubDataGridDataGrid_RowValidating(object? sender, RowValidatingEventArgs e)
    {
        RouteAddressViewModel? routeAddress = e.RowData as RouteAddressViewModel;
        if (routeAddress != null)
        {
            if (routeAddress.Address == null)
            {
                Debug.WriteLine("error address is blank");
                e.IsValid = false;
                e.ErrorMessages.Add("Address", "Error: Address cannot be blank");
            }
            else
            {
                Debug.WriteLine("its valid");
            }
        }
        else
        {
            Debug.WriteLine("routeAddress is null");
        }
    }

    public async void SubDataGrid_RowValidated(object? sender, RowValidatedEventArgs e)
    {
        RouteAddressViewModel? routeAddress = e.RowData as RouteAddressViewModel;
        if (routeAddress != null)
        {
            Debug.WriteLine("It's validated");
            //await SubPageViewModel.AddUpdate(routeAddress);
        }
        else
        {
            Debug.WriteLine("It's not validated");
        }
    }
}