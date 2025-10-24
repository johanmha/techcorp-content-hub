using Microsoft.AspNetCore.Mvc;
using TechCorp.ContentHub.API.ViewModels;
using TechCorp.ContentHub.Infrastructure.Services;

namespace TechCorp.ContentHub.API.Controllers;

public class BlogController : Controller
{
    private readonly IContentService _contentService;
    private readonly ILogger<BlogController> _logger;

    public BlogController(IContentService contentService, ILogger<BlogController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var blogPosts = await _contentService.GetBlogPostsAsync();

            var viewModel = new BlogListingViewModel
            {
                BlogPosts = blogPosts
                    .Select(BlogPostViewModel.FromModel)
                    .OrderByDescending(p => p.PublishedDate)
                    .ToList(),
                TotalPosts = blogPosts.Count()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading blog listing");
            return View("Error");
        }
    }

    public async Task<IActionResult> Detail(string slug)
    {
        try
        {
            var blogPost = await _contentService.GetBlogPostBySlugAsync(slug);

            if (blogPost == null)
            {
                return NotFound();
            }

            var viewModel = BlogPostViewModel.FromModel(blogPost);
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading blog post: {Slug}", slug);
            return View("Error");
        }
    }
}
