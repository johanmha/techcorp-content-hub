namespace TechCorp.ContentHub.Core.Models;

public class BlogPost
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Author? Author { get; set; }
    public List<Category> Categories { get; set; } = new();
    public DateTime PublishedDate { get; set; }
    public string FeaturedImage { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public int ReadingTime { get; set; }
}
