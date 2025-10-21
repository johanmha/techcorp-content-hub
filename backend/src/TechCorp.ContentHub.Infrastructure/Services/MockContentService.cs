using TechCorp.ContentHub.Core.Models;

namespace TechCorp.ContentHub.Infrastructure.Services;

public interface IContentService
{
    Task<IEnumerable<BlogPost>> GetBlogPostsAsync();
    Task<BlogPost?> GetBlogPostBySlugAsync(string slug);
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<IEnumerable<Category>> GetCategoriesAsync();
}

// Temporary mock service until Contentful is available
public class MockContentService : IContentService
{
    private readonly List<BlogPost> _mockPosts;
    
    public MockContentService()
    {
        _mockPosts = GenerateMockData();
    }
    
    public Task<IEnumerable<BlogPost>> GetBlogPostsAsync()
    {
        return Task.FromResult(_mockPosts.AsEnumerable());
    }
    
    public Task<BlogPost?> GetBlogPostBySlugAsync(string slug)
    {
        var post = _mockPosts.FirstOrDefault(p => p.Slug == slug);
        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        var authors = new List<Author>
        {
            new() {
                Id = "1",
                Name = "Sarah Johnson",
                Title = "Senior Developer",
                Bio = "10 years of enterprise development experience",
                Email = "sarah@techcorp.com"
            }
        };
        return Task.FromResult(authors.AsEnumerable());
    }
    
    public Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = new List<Category>
        {
            new() { Id = "1", Name = "Technology", Slug = "technology", Color = "#007bff" },
            new() { Id = "2", Name = "Best Practices", Slug = "best-practices", Color = "#28a745" }
        };
        return Task.FromResult(categories.AsEnumerable());
    }
    
    private List<BlogPost> GenerateMockData()
    {
        return new List<BlogPost>
        {
            new()
            {
                Id = "1",
                Title = "Getting Started with Contentful and .NET",
                Slug = "getting-started-contentful-dotnet",
                Summary = "Learn how to integrate Contentful CMS with your .NET applications",
                Content = "This is mock content. When Contentful is back online, this will be rich text content.",
                PublishedDate = DateTime.Now.AddDays(-5),
                ReadingTime = 5,
                Author = new Author { Id = "1", Name = "Sarah Johnson" },
                Categories = new List<Category> { new() { Name = "Technology" } },
                Tags = new List<string> { "CMS", "DotNet", "API" }
            },
            new()
            {
                Id = "2", 
                Title = "Enterprise React Patterns",
                Slug = "enterprise-react-patterns",
                Summary = "Best practices for building scalable React applications",
                Content = "Mock content about React patterns and architecture.",
                PublishedDate = DateTime.Now.AddDays(-2),
                ReadingTime = 8,
                Author = new Author { Id = "1", Name = "Sarah Johnson" },
                Categories = new List<Category> { new() { Name = "Best Practices" } },
                Tags = new List<string> { "React", "TypeScript", "Architecture" }
            }
        };
    }
}
