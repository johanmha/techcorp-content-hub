using TechCorp.ContentHub.Core.Models;

namespace TechCorp.ContentHub.API.ViewModels;

public class CategoryViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    public static CategoryViewModel FromModel(Category category)
    {
        return new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            Color = category.Color
        };
    }
}
