using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewOrder.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace NewOrder.Data;

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
            var connectionString = Environment.GetEnvironmentVariable("ORDERS_DATABASE");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}