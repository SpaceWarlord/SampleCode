using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleCode.Extensions;
using SampleCode.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data
{
    public partial class UserViewModel : DataViewModel
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Username is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than one character")]
        private string _username;

        public UserViewModel() { }
        public UserViewModel(int id, string username)
        {
            Id = id;
            Username = username;
        }

        /// <summary>
        /// Saves user data that has been edited.
        /// </summary>
        //public async Task<bool> SaveAsync()
        public async Task SaveAsync(UserService userService)
        {
            Debug.WriteLine("Called Save Async. Username: " + Username);
            IsModified = false;
            if (IsNew)
            {
                Debug.WriteLine("its new");

                IsNew = false;
                await userService.Add(this.ToDto());

                
            }            
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// 

        public async Task DeleteAsync()
        {
            //await App.Repository.Users.DeleteAsync(Id);
        }



        [RelayCommand]
        private void DeleteUser(int id)
        {
            Debug.WriteLine("called delete user");
            //Debug.WriteLine("id: " + id);
            //Users.Remove(user);
        }              
    }
}