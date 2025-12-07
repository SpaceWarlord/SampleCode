using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using SampleCode.Interfaces;
using SampleCode.Main;
using SampleCode.ViewModels.Data;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page
{
    public partial class LoginPageViewModel : PageViewModel, IPageViewModel<UserViewModel>
    {               
        public UserViewModel NewUser { get; set; }

        [ObservableProperty]
        public ObservableCollection<UserViewModel> _pageItemsList;

        public LoginPageViewModel()
        {                        
            PageItemsList = new ObservableCollection<UserViewModel>();
            NewUser = new UserViewModel(0, "");
            
        }

        [RelayCommand]
        private void Login(UserViewModel user)
        {
            Debug.WriteLine("-- Login --");
            if (user != null)
            {                
                Application.Current.Resources["currentUser"] = user;
                
                Window mainWindow = (Application.Current as App)?.MainWindow as Shell;
                mainWindow = new Shell();
                mainWindow.Activate();
                LoginWindow loginWindow = (Application.Current as App)?.LoginWindow as LoginWindow;
                if (loginWindow!=null)
                {
                    loginWindow.Close();
                }
                else
                {
                    Debug.WriteLine("Login window was null");
                }
            }
            else
            {
                Debug.WriteLine("User was null");
            }
        }        
                                                  
        public async Task LoadData()
        {
            PageItemsList.Clear();
            PageItemsList = new ObservableCollection<UserViewModel>(UserViewModel.GetAll());
        }

        [RelayCommand]
        public async Task GetAll()
        {
            UserViewModel.GetAll();
        }

        [RelayCommand]
        public async Task Add(UserViewModel viewModel)
        {
            await viewModel.Add();
        }

        [RelayCommand]
        public async Task Update(UserViewModel viewModel)
        {
            await viewModel.Update();
        }

        [RelayCommand]
        public Task Delete(UserViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}