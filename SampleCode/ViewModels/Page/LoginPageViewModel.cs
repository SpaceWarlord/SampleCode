using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Models;
using SampleCode.DTO;
using SampleCode.Main;
using SampleCode.Other;
using SampleCode.Services;
using SampleCode.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page
{
    public partial class LoginPageViewModel : PageViewModel
    {
        public ObservableCollection<UserViewModel> Users;

        public UserService UserService { get; set; }
        public UserViewModel NewUser { get; set; }
        public LoginPageViewModel()
        {
            UserService = new UserService(new SampleDbContext());
            Users = new ObservableCollection<UserViewModel>();
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

        /// <summary>
        /// Deletes user from database, then refreshes user list
        /// </summary>

        [RelayCommand]
        private async Task DeleteUser(UserViewModel user)
        {
            Debug.WriteLine("-- DeleteUser --");
            await user.DeleteAsync();
            await GetAll();

            //Users.Remove(user);
        }

        [RelayCommand]
        public async Task AddUser(object o)
        {
            Debug.WriteLine("-- AddUser --");
            if (o != null)
            {                
                string? username = o as string;                
                if (!string.IsNullOrWhiteSpace(username))
                {
                    Users.Clear();
                    ObservableCollection<UserDTO> userDTOList = await UserService.GetAll();
                    foreach (UserDTO userDTO in userDTOList)
                    {
                        Users.Add(new UserViewModel(userDTO.Id, userDTO.Username));
                    }
                    
                }
                else
                {
                    Debug.WriteLine("Username was empty or blank");
                }
            }
        }

        [RelayCommand]
        private void ModifyName(object o)
        {
            Debug.WriteLine("-- ModifiyName --");
            string oldName = "fred";
            
            if (o != null)
            {
                string newUsername = o as string;                
                if (newUsername != String.Empty)
                {                    
                    for (int i = Users.Count - 1; i >= 0; i--)
                    {                        
                        if (Users[i].Username == oldName)
                        {
                            Users[i].Username = newUsername;                                                       
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Textbox empty");
                }
            }
        }

        public void UpdateUsers(List<UserModel> users)
        {
            Users.Clear();            
            UserViewModel u;
            foreach (UserModel user in users)
            {
                u = new UserViewModel(user.Id, user.Username);
                u.Username = user.Username;                
                Users.Add(u);
            }
        }

        /// <summary>
        /// Saves user to database then clears fields 
        /// </summary>
        public async Task AddUserToDB()
        {
            Debug.WriteLine("--AddUserToDb--");
            await NewUser.SaveAsync(UserService);
            NewUser = new(0, "");
            await GetAll();
        }        

        public async Task GetAll()
        {
            ObservableCollection<UserDTO> userDTOList = await UserService.GetAll();                       
            Users.Clear();
            foreach (UserDTO userDTO in userDTOList)
            {                
                Users.Add(new UserViewModel(userDTO.Id, userDTO.Username));
            }
        }        
    }
}