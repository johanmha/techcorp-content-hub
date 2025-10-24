using Microsoft.AspNetCore.Mvc;
using TechCorp.ContentHub.API.ViewModels;
using TechCorp.ContentHub.Infrastructure.Services;

namespace TechCorp.ContentHub.API.Controllers;

public class AuthorsController : Controller
{
    private readonly IContentService _contentService;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(IContentService contentService, ILogger<AuthorsController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var authors = await _contentService.GetAuthorsAsync();

            var viewModel = new AuthorListingViewModel
            {
                Authors = authors
                    .Select(AuthorViewModel.FromModel)
                    .ToList(),
                TotalAuthors = authors.Count()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading authors listing");
            return View("Error");
        }
    }

    public async Task<IActionResult> Profile(string slug)
    {
        try
        {
            // Note: We need to add a GetAuthorBySlugAsync method to IContentService
            // For now, get all authors and filter by ID (treating slug as ID)
            var authors = await _contentService.GetAuthorsAsync();
            var author = authors.FirstOrDefault(a => a.Id == slug || a.Name.ToLower().Replace(" ", "-") == slug.ToLower());

            if (author == null)
            {
                return NotFound();
            }

            var viewModel = AuthorViewModel.FromModel(author);
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading author profile: {Slug}", slug);
            return View("Error");
        }
    }
}
