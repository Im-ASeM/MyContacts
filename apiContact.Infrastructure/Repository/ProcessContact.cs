using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kavenegar;
using Microsoft.IdentityModel.Tokens;

public class ProcessContact : IContact, IUserProcces
{
    Context db = new Context();
    public bool AddContact(NewContact newContact)
    {
        MyContacts added = new MyContacts { Name = newContact.Name, Number = newContact.Number, Email = newContact.Email, CreateTime = DateTime.Now };
        db.Tbl_Contact.Add(added);
        db.SaveChanges();
        return true;
    }

    public bool DelContact(int id)
    {
        MyContacts chose = db.Tbl_Contact.Find(id);
        if (chose == null)
        {
            return false;
        }
        else
        {
            db.Tbl_Contact.Remove(chose);
            db.SaveChanges();
            return true;
        }
    }

    public NewContact ShowContact(int id)
    {
        MyContacts chose = db.Tbl_Contact.Find(id);
        if (chose == null)
        {
            return null;
        }
        else
        {
            return new NewContact { Name = chose.Name, Number = chose.Number, Email = chose.Email };
        }
    }

    public List<NewContact> ShowContacts()
    {
        List<NewContact> results = new List<NewContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            NewContact result = new NewContact { Name = item.Name, Number = item.Number, Email = item.Email };
            results.Add(result);
        }
        return results;
    }

    public bool UpContact(Contact newContact)
    {
        MyContacts contact = db.Tbl_Contact.Find(newContact.Id);
        if (contact == null)
        {
            return false;
        }
        else
        {
            contact.Name = newContact.Name;
            contact.Number = newContact.Number;
            contact.Email = newContact.Email;
            db.Tbl_Contact.Update(contact);
            db.SaveChanges();
            return true;
        }
    }
    public List<FullDataContact> ShowDatas()
    {
        List<FullDataContact> results = new List<FullDataContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            FullDataContact result = new FullDataContact
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Email = item.Email,
                CreatTime = item.CreateTime
            };
            results.Add(result);
        }

        return results;
    }
    public List<IdContact> ShowId()
    {
        List<IdContact> results = new List<IdContact>();
        foreach (MyContacts item in db.Tbl_Contact.ToList())
        {
            IdContact result = new IdContact
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number
            };
            results.Add(result);
        }
        return results;
    }

    //---------------------------------------------------------------------- User Procces ------------------------------------------------
    private string paper = "Salam In PaPer Man Hastesh Khoda Qabol Kone";
    public string Login(LoginUser user)
    {
        Users check = db.Tbl_Users.FirstOrDefault(x => x.UserName == user.UserName);
        if (check == null)
        {
            return "Invalid User";
        }
        else if (!BCrypt.Net.BCrypt.Verify(user.Password + paper + user.UserName, check.Password))
        {
            return "Invalid Password";
        }
        else
        {
            return CreateToken(user.UserName);
        }
    }

    public string Register(NewUser user)
    {
        if (db.Tbl_Users.Any(x => x.UserName == user.UserName))
        {
            return "Invalid UserName";
        }
        else
        {
            Users result = new Users()
            {
                UserName = user.UserName,
                Phone = user.Phone,
                Name = user.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password + paper + user.UserName)
            };
            db.Tbl_Users.Add(result);
            db.SaveChanges();

            UserRoles resultRole = new UserRoles
            {
                UserID = db.Tbl_Users.FirstOrDefault(x => x.UserName == user.UserName).ID,
                RollID = db.Tbl_Roles.FirstOrDefault(x => x.RoleName == "user").ID
            };
            db.Tbl_UserRoles.Add(resultRole);

            db.SaveChanges();
            // return String.Format("{0} Added", user.UserName);
            return CreateToken(user.UserName);
        }
    }

    public List<AllUsers> GetAllUsers()
    {
        List<AllUsers> results = new List<AllUsers>();
        foreach (Users item in db.Tbl_Users.ToList())
        {
            results.Add(new AllUsers
            {
                ID = item.ID,
                UserName = item.UserName,
                Roll = String.Join(" , " , RollReader(item.UserName)),
                Name = item.Name,
                Phone = item.Phone
            });
        }
        return results;
    }

    public AnUser GetUser(string UserName)
    {
        Users Check = db.Tbl_Users.FirstOrDefault(x => x.UserName == UserName);
        return Check == null ? new AnUser() : new AnUser
        {
            ID = Check.ID,
            UserName = Check.UserName,
            Roll = String.Join(" , " , RollReader(Check.UserName)),
            Name = Check.Name,
            Phone = Check.Phone,
            CreateTime = Check.CreateTime,
            ModifyTime = Check.ModifyTime
        };
    }

    public string ResetPassword(string UserName)
    {
        Users Check = db.Tbl_Users.FirstOrDefault(x => x.UserName == UserName);
        if(Check == null){
            return "Invalid Username";
        }
        Random random = new Random();
        Check.SmsCode = random.Next(100000,999999).ToString();
        Check.isSmsValid = true;
        Check.ModifyTime = DateTime.Now;
        db.Tbl_Users.Update(Check);
        db.SaveChanges();

        // برای هزینه کمتر بدون اس ام اس
        return String.Format("Your Verfiy Code is {0}",Check.SmsCode);

        // با اس ام اس
        // KavenegarApi SmsApi = new KavenegarApi(db.SmsToken.Find(1).Token);
        // SmsApi.VerifyLookup(Check.Phone , Check.SmsCode , "demo");
        // return "Sms Sended";

    }

    public string VerfiyPassword(ResetPassUser user)
    {
        Users check = db.Tbl_Users.FirstOrDefault(x=> x.UserName == user.UserName);
        if(check == null){
            return "Invalid UserName";
        }
        else if(check.isSmsValid){
            if(user.Code != check.SmsCode){
                check.isSmsValid = false;
                db.Tbl_Users.Update(check);
                db.SaveChanges();
                return "Invalid Sms";
            }
            else{
                check.Password = BCrypt.Net.BCrypt.HashPassword(user.NewPassword + paper + user.UserName);
                check.isSmsValid = false;
                db.Tbl_Users.Update(check);
                db.SaveChanges();
                return "Password Updated";
            }
        }
        else{
            return "Invalid Sms";
        }
    }

    public string DeleteUser(string UserName)
    {
        Users check = db.Tbl_Users.FirstOrDefault(x=> x.UserName == UserName);
        if(check == null){
            return "Invalid User";
        }
        else{
            db.Tbl_Users.Remove(check);
            foreach (UserRoles item in db.Tbl_UserRoles.Where(x=> x.UserID == check.ID).ToList())   
            {
                db.Tbl_UserRoles.Remove(item);
            }
            db.SaveChanges();
            return "Done";
        }
    }


    public string Promote(string Username){
        Users Check = db.Tbl_Users.FirstOrDefault(x=>x.UserName==Username);
        int AdminId = db.Tbl_Roles.FirstOrDefault(y=> y.RoleName == "admin").ID;
        if(Check == null){
            return "Invalid User";
        }
        else if(db.Tbl_UserRoles.Any(x=> x.UserID == Check.ID && x.RollID == AdminId)){
            return String.Format("{0} Was Admin",Check.UserName);
        }
        else{
            db.Tbl_UserRoles.Add(new UserRoles{UserID = Check.ID , RollID = AdminId});
            db.SaveChanges();
            return String.Format("Done ! {0} is Admin Now",Check.UserName);
        }
    }

    public string Demote(string Username){
        Users Check = db.Tbl_Users.FirstOrDefault(x=> x.UserName == Username);
        int AdminId = db.Tbl_Roles.FirstOrDefault(x=> x.RoleName == "admin").ID;
        if (Check == null){
            return "Invalid User";
        }
        UserRoles checkRole = db.Tbl_UserRoles.FirstOrDefault(x=> x.UserID == Check.ID && x.RollID == AdminId);
        if(checkRole == null){
            return String.Format("{0} Was Not Admin" , Check.UserName);
        }
        else{
            db.Tbl_UserRoles.Remove(checkRole);
            db.SaveChanges();
            return String.Format("{0} Is Not Admin Anymore" , Check.UserName);
        }
    }
    private string CreateToken(string UserName)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Salam Dost Khobam In SecurityKey Man Hastesh"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claim = new Claim[]{
            new Claim(ClaimTypes.Name , UserName)
        };
        claim = claim.Concat(RollReader(UserName).Select(role=> new Claim(ClaimTypes.Role , role))).ToArray();

        JwtSecurityToken token = new JwtSecurityToken(
          issuer: "ASeM",
          audience:"ASeM",
          claims: claim,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: credentials
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    
    
    private string[] RollReader(string UserName)
    {
        Users check = db.Tbl_Users.FirstOrDefault(x=> x.UserName==UserName);
        if (check == null){ return Array.Empty<string>(); }

        string[] rolls = db.Tbl_UserRoles.Where(x=>x.UserID == check.ID).ToList().Select(y=> db.Tbl_Roles.Find(y.RollID).RoleName).ToArray();
        return rolls;
    }
}