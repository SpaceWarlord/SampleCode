using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Extensions.Navigation;
using SampleCode.Services.Navigation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation
{
    public partial class AddressViewModel : DataViewModel
    {
        [ObservableProperty]
        [Required]
        [NotNull]
        private string _name;

        [ObservableProperty]
        private string? _unitNum;

        [ObservableProperty]
        [Required]
        [NotNull]
        private string _streetNum;

        [ObservableProperty]
        [Required]
        [NotNull]
        private string _streetName;

        [ObservableProperty]
        [Required]
        [NotNull]
        private StreetTypeViewModel _streetType;

        [ObservableProperty]
        [Required]
        [NotNull]
        public SuburbViewModel _suburb;

        [ObservableProperty]
        private string _city;

        [ObservableProperty]
        private string? _gPS;
        
        public AddressViewModel() { }

        public AddressViewModel(int id, string name, string? unitNum, string streetNum, string streetName, StreetTypeViewModel streetType, SuburbViewModel suburb, string city, string? gps)
        {
            Id = id;
            Name = name;
            UnitNum = unitNum;
            StreetNum = streetNum;
            StreetName = streetName;
            StreetType = streetType;
            Suburb = suburb;
            City = "Adelaide";
            GPS = gps;
        }

        public async Task AddUpdate(AddressService addressService)
        {
            Debug.WriteLine("-- AddUpdate --");
            IsModified = false;
            if (IsNew)
            {                
                IsNew = false;
                int result = await addressService.AddUpdate(this.ToDto());
                if (result != 0)
                {
                    Id = result;
                }
                else
                {
                    Debug.WriteLine("Error Adding");
                }
            }
        }
    }
}