
using Orderos.Entities;

namespace Orderos.Models;

public class SavedCourseModel
{
    public SavedCoursesEntity SavedCourse { get; set; }
    public Course? Course { get; set; }
    public User? User { get; set; }
}
