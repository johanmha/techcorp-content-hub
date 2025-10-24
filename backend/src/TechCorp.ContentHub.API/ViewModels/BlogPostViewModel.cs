using TechCorp.ContentHub.Core.Models;

namespace TechCorp.ContentHub.API.ViewModels;

public class BlogPostViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public AuthorViewModel? Author { get; set; }
    public List<CategoryViewModel> Categories { get; set; } = new();
    public DateTime PublishedDate { get; set; }
    public string FeaturedImage { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public int ReadingTime { get; set; }

    public static BlogPostViewModel FromModel(BlogPost blogPost)
    {
        return new BlogPostViewModel
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Slug = blogPost.Slug,
            Summary = blogPost.Summary,
            Content = blogPost.Content,
            Author = blogPost.Author != null ? AuthorViewModel.FromModel(blogPost.Author) : null,
            Categories = blogPost.Categories.Select(CategoryViewModel.FromModel).ToList(),
            PublishedDate = blogPost.PublishedDate,
            FeaturedImage = blogPost.FeaturedImage,
            Tags = blogPost.Tags,
            ReadingTime = blogPost.ReadingTime
        };
    }
}
