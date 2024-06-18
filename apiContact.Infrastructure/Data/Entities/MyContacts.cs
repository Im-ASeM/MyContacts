using System.ComponentModel.DataAnnotations;

public class MyContacts
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public DateTime CreateTime { get; set; }
    public MyContacts()
    {
        CreateTime = CreateTime == null ? DateTime.Now : CreateTime;
    }
}