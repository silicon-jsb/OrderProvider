
using Microsoft.EntityFrameworkCore;
using Orderos.Entities;

namespace Orderos.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<SavedCoursesEntity> SavedCourses { get; set; }
}
