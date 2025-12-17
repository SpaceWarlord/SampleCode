using Microsoft.EntityFrameworkCore;
using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Other;

[Table("User", Schema = "TPT")]
[Index(nameof(Username), IsUnique = true)]
public class UserModel:IModel
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public int? SettingId { get; set; }

    [ForeignKey("SettingId")]
    public SettingModel? CurrentSetting { get; set; }

    public bool DefaultUser { get; set; } = false;

    public UserModel()
    {

    }
}
