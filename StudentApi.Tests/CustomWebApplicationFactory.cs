using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using StudentApi.Data;

namespace StudentApi.Tests;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"StudentApiTests-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var databaseRegistrations = services
                .Where(descriptor => descriptor.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    || descriptor.ServiceType == typeof(IDbContextOptionsConfiguration<AppDbContext>))
                .ToList();

            foreach (var registration in databaseRegistrations)
            {
                services.Remove(registration);
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));
        });
    }
}
