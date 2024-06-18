public class NewUser
{
    private string _username;
    public string UserName {
        get{
            return _username.ToLower();
        }
        set{
            _username=value;
        }
    }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    
}