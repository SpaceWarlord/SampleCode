using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Navigation
{
    [Table("Route", Schema = "TPT")]
    public class RouteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RouteAddressModel>? RouteAddresses { get; set; }
        public float Distance { get; set; }
        public RouteModel()
        {

        }
    }
}
