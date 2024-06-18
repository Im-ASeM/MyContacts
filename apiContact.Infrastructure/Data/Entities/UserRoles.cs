using System.ComponentModel.DataAnnotations;

public class UserRoles
{
    [Key]
    public int ID { get; set; }
    public int UserID { get; set; }
    public int RollID { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public UserRoles()
    {
        CreateTime = CreateTime == DateTime.MinValue ? DateTime.Now : CreateTime;
        ModifyTime = ModifyTime == DateTime.MinValue ? DateTime.Now : ModifyTime;
    }
}