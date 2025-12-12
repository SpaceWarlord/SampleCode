using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Navigation;

[Table("Suburb", Schema = "TPT")]
public class SuburbModel:IModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string PostCode { get; set; }

    public List<AddressModel> Addresses { get; set; }

    public SuburbModel()
    {

    }
}
