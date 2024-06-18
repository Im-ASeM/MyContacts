public class ResetPassUser
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
    public string NewPassword { get; set; }
    public string Code { get; set; }
}