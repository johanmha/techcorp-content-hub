namespace TechCorp.ContentHub.API.ViewModels;

public class CategoryListingViewModel
{
    public List<CategoryViewModel> Categories { get; set; } = new();
    public int TotalCategories { get; set; }
}
