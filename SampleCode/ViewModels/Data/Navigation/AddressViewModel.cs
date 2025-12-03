using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Models.Navigation;
using System.ComponentModel.DataAnnotations;
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
            City = "New York";
            GPS = gps;
        }        

        public async Task Add()
        {
            var db = new SampleDbContext();
            var model = new AddressModel()
            {
                Name = Name,
                UnitNum = UnitNum,
                StreetNum = StreetNum,
                StreetName = StreetName,
                StreetTypeId = StreetType.Id,
                SuburbId = Suburb.Id,
            };
            db.Addresses.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Update()
        {
            if (Id != 0)
            {
                var db = new SampleDbContext();
                var model = new AddressModel()
                {
                    Id = Id,
                    Name = Name,
                    UnitNum = UnitNum,
                    StreetNum = StreetNum,
                    StreetName = StreetName,
                    StreetTypeId = StreetType.Id,
                    SuburbId = Suburb.Id,
                };                               
                db.Addresses.Update(model);
                await db.SaveChangesAsync();
            }
        }
    }
}