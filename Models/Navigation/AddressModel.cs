using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Navigation
{
    [Table("Address", Schema = "TPT")]
    public class AddressModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UnitNum { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }

        public int StreetTypeId { get; set; }

        [ForeignKey("StreetTypeId")]
        public StreetTypeModel StreetType { get; set; }
        public int SuburbId { get; set; }

        [ForeignKey("SuburbId")]
        public SuburbModel Suburb { get; set; }
        public string City { get; set; } = "Adelaide";
        public string? GPS { get; set; }

        public List<RouteAddressModel> RouteAddresses { get; set; }

        public AddressModel() { }
    }
}
