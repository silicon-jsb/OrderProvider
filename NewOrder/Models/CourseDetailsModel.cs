
namespace NewOrder.Models;


public class CourseDetailsModel
{
    public string? Id { get; set; }
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public List<string>? Categories { get; set; }
    public string? Title { get; set; }
    public string? Ingress { get; set; }
    public int StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? Likes { get; set; }
    public string? LikesInPercent { get; set; }
    public string? Hours { get; set; }
    public List<CourseAuthor>? Authors { get; set; }
    public CoursePrice? Prices { get; set; }
    public CourseContent? Content { get; set; }
}

public class CourseAuthor
{
    public string? Name { get; set; }
    public string? AuthorImage { get; set; }
}

public class CoursePrice
{
    public string? Currency { get; set; }
    public double Price { get; set; }
    public int Discount { get; set; }
}

public class CourseContent
{
    public string? Description { get; set; }
    public List<string>? Includes { get; set; }
    public List<ProgramDetail>? ProgramDetails { get; set; }
}

public class ProgramDetail
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}

