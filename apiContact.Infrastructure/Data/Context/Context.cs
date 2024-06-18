using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<MyContacts> Tbl_Contact { get; set; }
    public DbSet<Roles> Tbl_Roles { get; set; }
    public DbSet<UserRoles> Tbl_UserRoles { get; set; }
    public DbSet<Users> Tbl_Users { get; set; }
    public DbSet<sms> SmsToken { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.\\SQL2019;database=contactsDB;trusted_connection=true;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }
}
