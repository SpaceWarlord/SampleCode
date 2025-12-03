using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class StreetTypeViewModel : DataViewModel
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
    }
}
