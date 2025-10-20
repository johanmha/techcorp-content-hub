using Serilog;
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

// Register our services - using Mock for now
builder.Services.AddSingleton<IContentService, MockContentService>();

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

app.Run();
