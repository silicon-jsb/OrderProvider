
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class SavedCourseEntity
{
	
	public string UserId { get; set; } = null!;

	[ForeignKey("UserId")]
	public UserEntity User { get; set; } = null!;

	public int CourseId { get; set; }

	[ForeignKey("CourseId")]
	public CourseEntity Course { get; set; } = null!;
}
