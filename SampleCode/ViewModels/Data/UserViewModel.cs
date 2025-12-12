using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using SampleCode.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data
{
    public partial class UserViewModel : DataViewModel, IViewModel<UserViewModel>
    {       
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

        public Task Add()
        {
            throw new System.NotImplementedException();
        }

        public Task Update()
        {
            throw new System.NotImplementedException();
        }

        public Task Delete()
        {
            throw new System.NotImplementedException();
        }

        public static IQueryable<UserViewModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<UserViewModel> query = db.Users.Select(c => new UserViewModel(c.Id, c.Username));
            return query;
        }
    }
}