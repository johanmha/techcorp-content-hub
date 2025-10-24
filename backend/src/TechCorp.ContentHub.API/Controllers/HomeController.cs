using Microsoft.AspNetCore.Mvc;
using TechCorp.ContentHub.API.ViewModels;
using TechCorp.ContentHub.Infrastructure.Services;

namespace TechCorp.ContentHub.API.Controllers;

public class HomeController : Controller
{
    private readonly IContentService _contentService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IContentService contentService, ILogger<HomeController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var blogPosts = await _contentService.GetBlogPostsAsync();
            var categories = await _contentService.GetCategoriesAsync();

            var viewModel = new HomeViewModel
            {
                FeaturedPosts = blogPosts
                    .Take(6)
                    .Select(BlogPostViewModel.FromModel)
                    .ToList(),
                Categories = categories
                    .Select(CategoryViewModel.FromModel)
                    .ToList()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading home page");
            return View("Error");
        }
    }
}
