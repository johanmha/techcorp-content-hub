using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Search;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using TechCorp.ContentHub.Core.Configuration;
using TechCorp.ContentHub.Core.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace TechCorp.ContentHub.Infrastructure.Services;

public class ContentfulService : IContentService
{
    private readonly IContentfulClient _client;
    private readonly ILogger<ContentfulService> _logger;

    public ContentfulService(
        IHttpClientFactory httpClientFactory,
        IOptions<ContentfulSettings> settings,
        ILogger<ContentfulService> logger)
    {
        _logger = logger;

        var contentfulOptions = new ContentfulOptions
        {
            DeliveryApiKey = settings.Value.DeliveryApiKey,
            PreviewApiKey = settings.Value.PreviewApiKey,
            SpaceId = settings.Value.SpaceId,
            Environment = settings.Value.Environment,
            UsePreviewApi = settings.Value.UsePreviewApi
        };

        var httpClient = httpClientFactory.CreateClient();
        _client = new ContentfulClient(httpClient, contentfulOptions);
    }

    public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching blog posts from Contentful");
            
            var queryBuilder = QueryBuilder<dynamic>.New
                .ContentTypeIs("blogPost")
                .Include(3); // Include referenced content up to 3 levels deep
                
            var entries = await _client.GetEntries(queryBuilder);
            
            var blogPosts = new List<BlogPost>();
            
            foreach (var entry in entries)
            {
                blogPosts.Add(MapToBlogPost(entry));
            }
            
            _logger.LogInformation("Successfully fetched {Count} blog posts", blogPosts.Count);
            return blogPosts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching blog posts from Contentful");
            throw;
        }
    }

    public async Task<BlogPost?> GetBlogPostBySlugAsync(string slug)
    {
        try
        {
            _logger.LogInformation("Fetching blog post with slug: {Slug}", slug);
            
            var queryBuilder = QueryBuilder<dynamic>.New
                .ContentTypeIs("blogPost")
                .FieldEquals("fields.slug", slug)
                .Include(3);
                
            var entries = await _client.GetEntries(queryBuilder);
            var entry = entries.FirstOrDefault();
            
            if (entry == null)
            {
                _logger.LogWarning("Blog post with slug {Slug} not found", slug);
                return null;
            }
            
            return MapToBlogPost(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching blog post with slug: {Slug}", slug);
            throw;
        }
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching authors from Contentful");

            var queryBuilder = QueryBuilder<dynamic>.New
                .ContentTypeIs("author");

            var entries = await _client.GetEntries(queryBuilder);

            var authors = new List<Author>();
            foreach (var entry in entries)
            {
                authors.Add(MapToAuthor(entry));
            }

            _logger.LogInformation("Successfully fetched {Count} authors", authors.Count);
            return authors;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching authors from Contentful");
            throw;
        }
    }

    public async Task<Author?> GetAuthorByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Fetching author with ID: {Id}", id);

            var entry = await _client.GetEntry<dynamic>(id);

            if (entry == null)
            {
                _logger.LogWarning("Author with ID {Id} not found", id);
                return null;
            }

            return MapToAuthor(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching author with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching categories from Contentful");

            var queryBuilder = QueryBuilder<dynamic>.New
                .ContentTypeIs("category");

            var entries = await _client.GetEntries(queryBuilder);

            var categories = entries.Select(MapToCategory).ToList();

            _logger.LogInformation("Successfully fetched {Count} categories", categories.Count);
            return categories;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching categories from Contentful");
            throw;
        }
    }

    public async Task<Category?> GetCategoryBySlugAsync(string slug)
    {
        try
        {
            _logger.LogInformation("Fetching category with slug: {Slug}", slug);

            var queryBuilder = QueryBuilder<dynamic>.New
                .ContentTypeIs("category")
                .FieldEquals("fields.slug", slug);

            var entries = await _client.GetEntries(queryBuilder);
            var entry = entries.FirstOrDefault();

            if (entry == null)
            {
                _logger.LogWarning("Category with slug {Slug} not found", slug);
                return null;
            }

            return MapToCategory(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching category with slug: {Slug}", slug);
            throw;
        }
    }

    private BlogPost MapToBlogPost(dynamic entry)
    {
        if (entry == null)
        {
            return new BlogPost { Id = "", Title = "", Slug = "", Summary = "", Content = "" };
        }

        JObject jObj = entry as JObject ?? JObject.FromObject(entry);

        var blogPost = new BlogPost
        {
            Id = jObj["sys"]?["id"]?.ToString() ?? "",
            Title = jObj["title"]?.ToString() ?? "",
            Slug = jObj["slug"]?.ToString() ?? "",
            Summary = jObj["summary"]?.ToString() ?? "",
            Content = ConvertRichTextToHtml(jObj["content"]),
            PublishedDate = jObj["publishedDate"] != null
                ? DateTime.Parse(jObj["publishedDate"].ToString())
                : DateTime.Now,
            ReadingTime = jObj["readingTime"] != null
                ? jObj["readingTime"].Value<int>()
                : 5,
            FeaturedImage = GetImageUrl(jObj["featuredImage"]),
            Tags = GetTags(jObj["tags"])
        };

        // Map Author if exists
        var authorToken = jObj["author"];
        if (authorToken != null)
        {
            blogPost.Author = MapToAuthor(authorToken);
        }

        // Map Categories if exist
        var categoriesToken = jObj["categories"];
        if (categoriesToken != null && categoriesToken is JArray categoriesArray)
        {
            blogPost.Categories = new List<Category>();
            foreach (var category in categoriesArray)
            {
                blogPost.Categories.Add(MapToCategory(category));
            }
        }

        return blogPost;
    }

    private Author MapToAuthor(dynamic entry)
    {
        try
        {
            if (entry == null)
            {
                _logger.LogWarning("Null entry passed to MapToAuthor");
                return new Author { Id = "", Name = "", Bio = "", Email = "", Title = "", ProfileImage = "", SocialLinks = new List<string>() };
            }

            // Cast to JObject to access JSON properties
            JObject jObj = entry as JObject ?? JObject.FromObject(entry);

            // Extract ID from sys object
            string id = jObj["sys"]?["id"]?.ToString() ?? "";

            // Extract fields directly from the JObject
            string name = jObj["name"]?.ToString() ?? "";
            string bio = jObj["bio"]?.ToString() ?? "";
            string email = jObj["email"]?.ToString() ?? "";
            string title = jObj["title"]?.ToString() ?? "";

            // Handle profile image
            string profileImage = "";
            var profileImageToken = jObj["profileImage"];
            if (profileImageToken != null)
            {
                profileImage = GetImageUrl(profileImageToken);
            }

            var author = new Author
            {
                Id = id,
                Name = name,
                Bio = bio,
                Email = email,
                Title = title,
                ProfileImage = profileImage,
                SocialLinks = new List<string>()
            };

            // Map social links if they exist
            var socialLinksToken = jObj["socialLinks"];
            if (socialLinksToken != null)
            {
                try
                {
                    var twitter = socialLinksToken["twitter"]?.ToString();
                    var linkedin = socialLinksToken["linkedin"]?.ToString();
                    var github = socialLinksToken["github"]?.ToString();

                    if (!string.IsNullOrEmpty(twitter))
                        author.SocialLinks.Add(twitter);
                    if (!string.IsNullOrEmpty(linkedin))
                        author.SocialLinks.Add(linkedin);
                    if (!string.IsNullOrEmpty(github))
                        author.SocialLinks.Add(github);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error mapping social links");
                }
            }

            _logger.LogDebug("Mapped author: Id={Id}, Name={Name}", author.Id, author.Name);
            return author;
        }
        catch (Exception ex)
        {
            string typeName = "unknown";
            try { typeName = entry?.GetType()?.Name ?? "unknown"; } catch { }
            _logger.LogError(ex, "Error mapping author entry. Entry type: {Type}", typeName);
            throw;
        }
    }

    private Category MapToCategory(dynamic entry)
    {
        if (entry == null)
        {
            return new Category { Id = "", Name = "", Slug = "", Description = "", Color = "#007bff" };
        }

        JObject jObj = entry as JObject ?? JObject.FromObject(entry);

        return new Category
        {
            Id = jObj["sys"]?["id"]?.ToString() ?? "",
            Name = jObj["name"]?.ToString() ?? "",
            Slug = jObj["slug"]?.ToString() ?? "",
            Description = jObj["description"]?.ToString() ?? "",
            Color = jObj["color"]?.ToString() ?? "#007bff"
        };
    }

    private string GetImageUrl(dynamic asset)
    {
        if (asset == null) return string.Empty;

        try
        {
            // Handle JToken (from Contentful JSON response)
            if (asset is JToken token)
            {
                var url = token["file"]?["url"]?.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    // Contentful URLs start with // so we need to add https:
                    if (url.StartsWith("//"))
                    {
                        url = "https:" + url;
                    }
                    return url;
                }
            }

            // Fallback to dynamic property access
            var fields = asset.Fields ?? asset.fields;
            if (fields?.file?.url != null)
            {
                var url = fields.file.url.ToString();
                if (url.StartsWith("//"))
                {
                    url = "https:" + url;
                }
                return url;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error extracting image URL from asset");
        }

        return string.Empty;
    }

    private List<string> GetTags(dynamic tags)
    {
        var tagList = new List<string>();
        if (tags != null)
        {
            foreach (var tag in tags)
            {
                tagList.Add(tag.ToString());
            }
        }
        return tagList;
    }

    private string ConvertRichTextToHtml(dynamic richText)
    {
        // For now, return a simple string representation
        // In production, you'd use Contentful's rich text renderer
        if (richText?.content != null)
        {
            var htmlContent = new List<string>();
            foreach (var node in richText.content)
            {
                if (node.nodeType == "paragraph" && node.content != null)
                {
                    var paragraph = "<p>";
                    foreach (var content in node.content)
                    {
                        if (content.value != null)
                        {
                            paragraph += content.value.ToString();
                        }
                    }
                    paragraph += "</p>";
                    htmlContent.Add(paragraph);
                }
                else if (node.nodeType == "heading-1" && node.content != null)
                {
                    var heading = "<h1>";
                    foreach (var content in node.content)
                    {
                        if (content.value != null)
                        {
                            heading += content.value.ToString();
                        }
                    }
                    heading += "</h1>";
                    htmlContent.Add(heading);
                }
                // Add more node types as needed
            }
            return string.Join("\n", htmlContent);
        }
        return "";
    }
}
