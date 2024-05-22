using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orderos.Data;
using Orderos.Entities;
using Orderos.Factories;

namespace Orderos.Services;

public class CourseService
{
	private readonly DataContext _context;
	private readonly ILogger<CourseService> _logger;

	public CourseService(DataContext context, ILogger<CourseService> logger)
	{
		_context = context;
		_logger = logger;
	}

	//get from backoffice
	public async Task<ServiceResponse<IEnumerable<SavedCoursesEntity>>> GetAllSavedCourses()
	{
		var serviceResponse = new ServiceResponse<IEnumerable<SavedCoursesEntity>>();
		try
		{
			serviceResponse.Data = await _context.SavedCourses.ToListAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while getting saved courses.");
			serviceResponse.Success = false;
			serviceResponse.Message = "An error occurred while getting saved courses.";
		}
		return serviceResponse;
	}

	//get all from clientside
	public async Task<ServiceResponse<IEnumerable<SavedCoursesEntity>>> GetSavedCourseById(string userId)
	{
		var serviceResponse = new ServiceResponse<IEnumerable<SavedCoursesEntity>>();
		try
		{
			serviceResponse.Data = await _context.SavedCourses
				.Include(sc => sc.CourseId)
				.Where(sc => sc.UserId == userId)
				.ToListAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"An error occurred while getting saved courses for user {userId}.");
			serviceResponse.Success = false;
			serviceResponse.Message = $"An error occurred while getting saved courses for user {userId}.";
		}
		return serviceResponse;
	}

	//get one from clientside
	public async Task<ServiceResponse<SavedCoursesEntity>> GetSavedCourseById(int id)
	{
		ServiceResponse<SavedCoursesEntity> response = new ServiceResponse<SavedCoursesEntity>();
		try
		{
			response.Data = await _context.SavedCourses.FindAsync(id);
			if (response.Data == null)
			{
				response.Success = false;
				response.Message = $"Saved course with ID {id} not found.";
			}
		}
		catch (Exception ex)
		{
			response.Success = false;
			response.Message = $"An error occurred while fetching the saved course: {ex.Message}";
		}

		return response;
	}


	//post from clientside
	public async Task<ServiceResponse<SavedCoursesEntity>> SaveCourse(SavedCoursesEntity savedCourse)
	{
		var serviceResponse = new ServiceResponse<SavedCoursesEntity>();
		try
		{
			_context.SavedCourses.Add(savedCourse);
			await _context.SaveChangesAsync();
			serviceResponse.Data = savedCourse;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while saving the course.");
			serviceResponse.Success = false;
			serviceResponse.Message = "An error occurred while saving the course.";
		}
		return serviceResponse;
	}


	//delete from clientside
	public async Task<ServiceResponse<bool>> DeleteSavedCourse(SavedCoursesEntity savedCourse)
	{
		var serviceResponse = new ServiceResponse<bool>();
		try
		{
			_context.SavedCourses.Remove(savedCourse);
			await _context.SaveChangesAsync();
			serviceResponse.Data = true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while deleting the course.");
			serviceResponse.Success = false;
			serviceResponse.Message = "An error occurred while deleting the course.";
			serviceResponse.Data = false;
		}
		return serviceResponse;
	}
}

