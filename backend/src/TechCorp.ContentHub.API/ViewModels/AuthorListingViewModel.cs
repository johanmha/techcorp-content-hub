namespace TechCorp.ContentHub.API.ViewModels;

public class AuthorListingViewModel
{
    public List<AuthorViewModel> Authors { get; set; } = new();
    public int TotalAuthors { get; set; }
}
