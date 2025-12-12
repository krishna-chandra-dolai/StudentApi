using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Profiles;
using StudentApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers + FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation();

// Register FluentValidation validators from current assembly
builder.Services.AddValidatorsFromAssemblyContaining<StudentDtoValidator>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
