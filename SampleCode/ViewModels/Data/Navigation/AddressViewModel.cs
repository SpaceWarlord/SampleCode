using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using Syncfusion.UI.Xaml.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data.Navigation;

public partial class AddressViewModel : DataViewModel, IViewModel<AddressViewModel>
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
        Debug.WriteLine("saved address count " + db.Addresses.Count());
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

    public async Task Delete()
    {
        var db = new SampleDbContext();
        db.Remove(Id);
        await db.SaveChangesAsync();
    }    
    public static IQueryable<AddressViewModel> GetAll()
    {
        var db = new SampleDbContext();
        IQueryable<AddressViewModel> query = db.Addresses.Select(c => new AddressViewModel(c.Id, c.Name, c.UnitNum, c.StreetNum, c.StreetName, 
            new StreetTypeViewModel(c.StreetType.Id, c.StreetType.Code, c.StreetType.Name, c.StreetType.Common), 
            new SuburbViewModel(c.Suburb.Id, c.Suburb.Name, c.Suburb.PostCode), c.City, c.GPS));
        return query;
    }   
}