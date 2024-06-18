using System.ComponentModel.DataAnnotations;

public class Users
{
    [Key]
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public bool isSmsValid { get; set; }
    public string SmsCode { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public Users()
    {
        SmsCode = SmsCode == null ? "-1" : SmsCode;
        CreateTime = CreateTime == DateTime.MinValue ? DateTime.Now : CreateTime;
        ModifyTime = ModifyTime == DateTime.MinValue ? DateTime.Now : ModifyTime;
    }
}