using Serilog;
using TechCorp.ContentHub.Core.Configuration;
using TechCorp.ContentHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpClient support
builder.Services.AddHttpClient();

// Configure Contentful with our renamed settings class
builder.Services.Configure<ContentfulSettings>(
    builder.Configuration.GetSection("Contentful"));

// Register Contentful service (not mock anymore!)
builder.Services.AddSingleton<IContentService, ContentfulService>();

// Add response caching
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache(); // For future caching

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error/500");
    app.UseHsts();
}

// Handle 404 errors
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files from wwwroot
app.UseRouting();
app.UseResponseCaching();
app.UseAuthorization();

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom routes for better URLs
app.MapControllerRoute(
    name: "blogDetail",
    pattern: "blog/{slug}",
    defaults: new { controller = "Blog", action = "Detail" });

app.MapControllerRoute(
    name: "authorProfile",
    pattern: "authors/{slug}",
    defaults: new { controller = "Authors", action = "Profile" });

app.MapControllerRoute(
    name: "categoryDetail",
    pattern: "categories/{slug}",
    defaults: new { controller = "Categories", action = "Category" });

app.Run();
