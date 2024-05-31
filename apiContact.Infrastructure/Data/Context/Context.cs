using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<MyContacts> Tbl_Contact { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.\\SQL2019;database=contactsDB;trusted_connection=true;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }
}
