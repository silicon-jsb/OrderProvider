
namespace NewOrder.Models;

public class CourseCardModel
{
    public string Id { get; set; } = null!;
    public string? ImageUri { get; set; }
    public bool IsBestseller { get; set; }
    public string? Title { get; set; }
    public string? Likes { get; set; }
    public string? LikesInPercent { get; set; }
    public string? Hours { get; set; }
    public string? AuthorName { get; set; }
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
