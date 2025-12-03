using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using SampleCode.ViewModels.Page.Navigation;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SampleCode.Views.Navigation
{
    public sealed partial class AddressPage : Page, IDataGrid<AddressPageViewModel>
    {
        public AddressPageViewModel PageViewModel { get; set; }
        public AddressPage()
        {
            this.InitializeComponent();
            PageViewModel = new AddressPageViewModel();
            this.DataContext = PageViewModel;
            MainDataGrid.AddNewRowInitiating += MainDataGrid_AddNewRowInitiating;
            MainDataGrid.DataValidationMode = Syncfusion.UI.Xaml.Grids.GridValidationMode.InView;
            MainDataGrid.RowValidated += MainDataGrid_RowValidated;
            MainDataGrid.RowValidating += MainDataGrid_RowValidating;
        }

        public async void OnPageLoad(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("-- OnPageLoad --");
            await PageViewModel.LoadData();
            MainDataGrid.ItemsSource = PageViewModel.PageItemsList;            
        }

        public void OnMainGridLoad(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("-- OnMainGridLoad --");            
        }

        public void MainDataGrid_AddNewRowInitiating(object? sender, AddNewRowInitiatingEventArgs e)
        {
            Debug.WriteLine("-- AddNewRowInitiating --");
            var address = e.NewObject as AddressViewModel;
            if (address != null)
            {
                var name = e.NewObject.GetType().GetProperty("Name").GetValue(e.NewObject);
                if (name != null)
                {
                    if (string.IsNullOrWhiteSpace(name.ToString()))
                    {
                        Debug.WriteLine("Error adding - name was blank");
                    }
                    else
                    {
                        Debug.WriteLine("Adding. Name is " + name);
                    }                    
                    //await ViewModel.AddClientToDB();
                }
            }
            else
            {
                Debug.WriteLine("address was null");
            }
        }

        public void MainDataGrid_RowValidating(object? sender, RowValidatingEventArgs e)
        {
            AddressViewModel? address = e.RowData as AddressViewModel;
            if (address != null)
            {
                if (string.IsNullOrWhiteSpace(address.Name))
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("Name", "Error: Name cannot be blank");
                }
                if (string.IsNullOrWhiteSpace(address.StreetNum))
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("StreetNum", "Error: Street number cannot be blank");
                }
                if (string.IsNullOrWhiteSpace(address.StreetName))
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("StreetName", "Error: Street Name cannot be blank");
                }
                if (address.StreetType == null)
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("StreetType", "Error: Street Type cannot be blank");
                }
                if (address.Suburb == null)
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("Suburb", "Error: Suburb cannot be blank");
                }
            }
        }

        public async void MainDataGrid_RowValidated(object? sender, RowValidatedEventArgs e)
        {
            AddressViewModel? address = e.RowData as AddressViewModel;
            if (address != null)
            {
                await PageViewModel.AddUpdate(address);
            }
        }        
    }
}