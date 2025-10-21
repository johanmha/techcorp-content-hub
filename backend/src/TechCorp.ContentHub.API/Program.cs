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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpClient support
builder.Services.AddHttpClient();

// Configure Contentful with our renamed settings class
builder.Services.Configure<ContentfulSettings>(
    builder.Configuration.GetSection("Contentful"));

// Register Contentful service (not mock anymore!)
builder.Services.AddSingleton<IContentService, ContentfulService>();

// Add CORS for React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

// Add response caching
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache(); // For future caching

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactApp");
app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();

// Add a welcome endpoint
app.MapGet("/", () => "TechCorp Content Hub API - Visit /swagger for API documentation");

app.Run();
