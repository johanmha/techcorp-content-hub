namespace TechCorp.ContentHub.Core.Models;

public class Author  
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<string> SocialLinks { get; set; } = new();
}
