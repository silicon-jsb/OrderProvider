
using Microsoft.EntityFrameworkCore;
using Orderos.Entities;

namespace Orderos.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<SavedCoursesEntity> SavedCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:sqlserver-silicon-jsb.database.windows.net,1433;Initial Catalog=orders_newdatabase;Persist Security Info=False;User ID=SqlAdmin;Password=Bytmig123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
