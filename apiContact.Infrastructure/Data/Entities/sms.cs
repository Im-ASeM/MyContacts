using System.ComponentModel.DataAnnotations;

public class sms
{
    [Key]
    public int ID { get; set; }
    public string Token { get; set; }
}