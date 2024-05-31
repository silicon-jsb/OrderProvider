﻿
using NewOrder.Entities;

namespace NewOrder.Models;

public class SavedCourseModel
{
    public SavedCoursesEntity SavedCourse { get; set; }
    public CourseModel? Course { get; set; }
    public User? User { get; set; }
}
