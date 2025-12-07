using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using SampleCode.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class StreetTypeViewModel : DataViewModel, IViewModel<StreetTypeViewModel>
    {
        [Key]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Code is Required")]
        private string _code;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Name is Required")]
        private string _name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Common is Required")]
        private bool _common;

        public StreetTypeViewModel() { }

        public StreetTypeViewModel(int id, string code, string name, bool common)
        {
            Id = id;
            Code = code;
            Name = name;
            Common = common;
        }

        public static IQueryable<StreetTypeViewModel> GetAll()
        {
            var db = new SampleDbContext();
            IQueryable<StreetTypeViewModel> query = db.StreetTypes.Select(c => new StreetTypeViewModel(c.Id, c.Code, c.Name, c.Common));
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
