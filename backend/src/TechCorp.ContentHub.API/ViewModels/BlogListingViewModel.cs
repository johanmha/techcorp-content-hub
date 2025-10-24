namespace TechCorp.ContentHub.API.ViewModels;

public class BlogListingViewModel
{
    public List<BlogPostViewModel> BlogPosts { get; set; } = new();
    public int TotalPosts { get; set; }
}
