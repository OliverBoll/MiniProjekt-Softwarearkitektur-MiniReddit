using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using MiniProject.Data;
using MiniProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

app.UseHttpsRedirection();

app.UseCors(AllowSomeStuff);

app.MapGet("/", (DataService Service) =>
{
    return
        new { message = "Hello Josh" };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/api/posts", (DataService service) =>
{
    return service.GetPosts().Select(p => new
    {
        title = p.Title,
        date = p.Date,
        user = p.User.Name,
        vote = p.Vote,
        comment = p.Comment.Select(c => new { Text = c.Text })
    });
});

app.Run();
