using TechCorp.ContentHub.Core.Models;

namespace TechCorp.ContentHub.API.ViewModels;

public class AuthorViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<string> SocialLinks { get; set; } = new();

    public static AuthorViewModel FromModel(Author author)
    {
        return new AuthorViewModel
        {
            Id = author.Id,
            Name = author.Name,
            Bio = author.Bio,
            ProfileImage = author.ProfileImage,
            Email = author.Email,
            Title = author.Title,
            SocialLinks = author.SocialLinks
        };
    }
}
