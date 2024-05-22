using Microsoft.EntityFrameworkCore;
using Orderos.Data;
using Orderos.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orderos.Repositories;

public class SavedCoursesRepository
{
	private readonly DataContext _context;

	public SavedCoursesRepository(DataContext context)
	{
		_context = context;
	}

	//private readonly SavedCoursesRepository _savedCoursesRepository = savedCoursesRepository;

	public async Task<IEnumerable<SavedCoursesEntity>> GetSavedCoursesAsync(string userId)
	{
	return await _context.SavedCourses
		.Where(sc => sc.UserId == userId)		
		.ToListAsync();
	}

	//public async Task<IEnumerable<SavedCoursesEntity>> GetSavedCoursesAsync(string userId)
	//{
	//	return await _context.SavedCourses
	//		.Where(sc => sc.UserId == userId)
	//		.Include(sc => sc.UserName)
	//		.Include(sc => sc.CourseId)
	//		.Include(sc => sc.CourseTitle)
	//		.ToListAsync();
	//}
}