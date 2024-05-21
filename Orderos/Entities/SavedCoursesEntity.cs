
using System.ComponentModel.DataAnnotations.Schema;

namespace Orderos.Entities;

public class SavedCoursesEntity
{
	public int Id { get; set; }
	public string UserId { get; set; } = null!;
	//public string UserName { get; set; }
	public int CourseId { get; set; }
	public string? CourseTitle { get; set; }

}
