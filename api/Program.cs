using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using shared.Model;
using Service;
using Data;

var builder = WebApplication.CreateBuilder(args);

// Tilføj databasekonteksten (SQLite i dette tilfælde)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrer PostService og CommentService som dependencies
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Tilføj API controllere
builder.Services.AddControllers();

// Konfigurer CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Brug CORS middleware
app.UseCors("AllowAll");

// Brug API routing og endpoints
app.MapControllers(); // Dette aktiverer dine API routes

app.MapGet("/", () => "Welcome to the Reddit Clone API!");

// Start applikationen
app.Run();
