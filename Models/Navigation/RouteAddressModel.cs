using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Navigation;

[Table("RouteAddress", Schema = "TPT")]
public class RouteAddressModel:IModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int RouteId { get; set; }
    [ForeignKey("RouteId")]
    public RouteModel Route { get; set; }
    public int AddressId { get; set; }
    [ForeignKey("AddressId")]
    public AddressModel Address { get; set; }
    public int Order { get; set; }
    public RouteAddressModel() { }
}
