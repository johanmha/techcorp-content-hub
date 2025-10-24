namespace TechCorp.ContentHub.API.ViewModels;

public class HomeViewModel
{
    public List<BlogPostViewModel> FeaturedPosts { get; set; } = new();
    public List<CategoryViewModel> Categories { get; set; } = new();
}
