using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Navigation
{
    [Table("StreetType", Schema = "TPT")]
    public class StreetTypeModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Common { get; set; } = false;

        public List<AddressModel> Addresses { get; set; }

        public StreetTypeModel()
        {

        }
    }
}
