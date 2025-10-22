using TechCorp.ContentHub.Core.Models;

namespace TechCorp.ContentHub.Infrastructure.Services;

public interface IContentService
{
    Task<IEnumerable<BlogPost>> GetBlogPostsAsync();
    Task<BlogPost?> GetBlogPostBySlugAsync(string slug);
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(string id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryBySlugAsync(string slug);
}
