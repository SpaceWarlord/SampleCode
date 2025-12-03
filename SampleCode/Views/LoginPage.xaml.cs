using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SampleCode.ViewModels.Page;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPageViewModel ViewModel { get; } = new();

        private DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        public LoginPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel;            
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Grid Loaded");
            await ResetUserList();
        }

        /// <summary>
        /// Resets the user list.
        /// </summary>        
        private async Task ResetUserList()
        {            
            await dispatcherQueue.EnqueueAsync(async () =>
                await ViewModel.GetAll());
        }

        [RelayCommand]
        public void Test(int id)
        {
            Debug.WriteLine("Called test");
        }
    }
}