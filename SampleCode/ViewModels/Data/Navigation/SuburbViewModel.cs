using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class SuburbViewModel : DataViewModel, IViewModel<SuburbViewModel>
    {       
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Name is Required")]
        private string _name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Post Code is Required")]
        private string _postCode;

        public SuburbViewModel() { }

        public SuburbViewModel(int id, string name, string postCode)
        {
            Id = id;
            Name = name;
            PostCode = postCode;
        }

        public SuburbViewModel(SuburbModel model)
        {
            Id = model.Id;
            Name = model.Name;
            PostCode = model.PostCode;
        }

        public static IQueryable<SuburbViewModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<SuburbViewModel> query = db.Suburbs.Select(c => new SuburbViewModel(c.Id, c.Name, c.PostCode));
            return query;
        }

        public Task Add()
        {
            throw new System.NotImplementedException();
        }

        public Task Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
