namespace TechCorp.ContentHub.Core.Configuration;

public class ContentfulSettings
{
    public string SpaceId { get; set; } = string.Empty;
    public string DeliveryApiKey { get; set; } = string.Empty;
    public string PreviewApiKey { get; set; } = string.Empty;
    public string Environment { get; set; } = "master";
    public bool UsePreviewApi { get; set; } = false;
}
