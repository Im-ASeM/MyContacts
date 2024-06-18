public class LoginUser
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
}