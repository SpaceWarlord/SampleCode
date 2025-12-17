using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SampleCode.ViewModels.Page;
using System.Diagnostics;

namespace SampleCode.Views;

public sealed partial class LoginPage : Page
{
    public LoginPageViewModel ViewModel { get; }

    public LoginPage()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<LoginPageViewModel>();
        DataContext = ViewModel;            
    }

    private async void Grid_Loaded(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Grid Loaded");
        await ViewModel.LoadData();
       
    }    
}