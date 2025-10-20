using Microsoft.AspNetCore.Mvc;
using TechCorp.ContentHub.Core.Models;
using TechCorp.ContentHub.Infrastructure.Services;

namespace TechCorp.ContentHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly ILogger<ContentController> _logger;
    
    public ContentController(IContentService contentService, ILogger<ContentController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }
    
    [HttpGet("posts")]
    public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
    {
        _logger.LogInformation("Fetching all blog posts");
        var posts = await _contentService.GetBlogPostsAsync();
        return Ok(posts);
    }
    
    [HttpGet("posts/{slug}")]  
    public async Task<ActionResult<BlogPost>> GetBlogPostBySlug(string slug)
    {
        _logger.LogInformation("Fetching blog post with slug: {Slug}", slug);
        var post = await _contentService.GetBlogPostBySlugAsync(slug);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }
    
    [HttpGet("authors")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()  
    {
        var authors = await _contentService.GetAuthorsAsync();
        return Ok(authors);
    }
    
    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _contentService.GetCategoriesAsync();
        return Ok(categories);
    }
}
