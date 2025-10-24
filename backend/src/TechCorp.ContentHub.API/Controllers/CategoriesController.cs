using Microsoft.AspNetCore.Mvc;
using TechCorp.ContentHub.API.ViewModels;
using TechCorp.ContentHub.Infrastructure.Services;

namespace TechCorp.ContentHub.API.Controllers;

public class CategoriesController : Controller
{
    private readonly IContentService _contentService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(IContentService contentService, ILogger<CategoriesController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await _contentService.GetCategoriesAsync();

            var viewModel = new CategoryListingViewModel
            {
                Categories = categories
                    .Select(CategoryViewModel.FromModel)
                    .ToList(),
                TotalCategories = categories.Count()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading categories listing");
            return View("Error");
        }
    }

    public async Task<IActionResult> Category(string slug)
    {
        try
        {
            var category = await _contentService.GetCategoryBySlugAsync(slug);

            if (category == null)
            {
                return NotFound();
            }

            // Get all blog posts and filter by category
            var allPosts = await _contentService.GetBlogPostsAsync();
            var categoryPosts = allPosts.Where(p =>
                p.Categories.Any(c => c.Slug == slug)
            ).ToList();

            var viewModel = new CategoryDetailViewModel
            {
                Category = CategoryViewModel.FromModel(category),
                Posts = categoryPosts
                    .Select(BlogPostViewModel.FromModel)
                    .OrderByDescending(p => p.PublishedDate)
                    .ToList()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading category: {Slug}", slug);
            return View("Error");
        }
    }
}
