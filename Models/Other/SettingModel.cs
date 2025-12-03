using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleCode.Other
{
    [Table("Setting", Schema = "TPT")]
    public class SettingModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BaseFolder { get; set; }
        public bool DarkMode { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        public SettingModel()
        {

        }
    }
}
