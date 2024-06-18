public class AllUsers
{
    public int ID { get; set; }
    private string _username;
    public string UserName {
        get{
            return _username.ToLower();
        }
        set{
            _username=value;
        }
    }
    public string Roll { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}