namespace TechCorp.ContentHub.API.ViewModels;

public class CategoryDetailViewModel
{
    public CategoryViewModel Category { get; set; } = new();
    public List<BlogPostViewModel> Posts { get; set; } = new();
}
