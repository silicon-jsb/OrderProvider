using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Orderos.Entities;

namespace Orderos.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<SavedCoursesEntity> SavedCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = System.Environment.GetEnvironmentVariable("ORDERS_DATABASE");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
