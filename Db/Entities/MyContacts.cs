using System.ComponentModel.DataAnnotations;

public class MyContacts
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
}