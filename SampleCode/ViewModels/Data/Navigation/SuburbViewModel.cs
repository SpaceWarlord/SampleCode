using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class SuburbViewModel : DataViewModel
    {
        [Key]
        [ObservableProperty]
        private int _id;

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
    }
}
