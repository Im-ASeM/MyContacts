using Microsoft.VisualBasic.FileIO;

public interface IUserProcces
{
    public string Login(LoginUser user);
    public string Register(NewUser user);
    public List<AllUsers> GetAllUsers();
    public AnUser GetUser(string UserName);
    public string ResetPassword(string UserName);
    public string VerfiyPassword(ResetPassUser user);
    public string DeleteUser(string UserName);
    public string Promote(string Username);
    public string Demote(string Username);

}