using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Register FluentValidation validators from current assembly
builder.Services.AddValidatorsFromAssemblyContaining<StudentDtoValidator>();

// Repository DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// Service DI
builder.Services.AddScoped<IStudentService, StudentService>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve the browser demo in wwwroot/test.html.
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Exposes the generated entry point to WebApplicationFactory integration tests.
public partial class Program { }
