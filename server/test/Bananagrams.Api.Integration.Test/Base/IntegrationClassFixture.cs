using Bananagrams.Dal.Contexts;
using Bananagrams.Dal.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Bananagrams.Api.Integration.Test.Base;

public class IntegrationClassFixture : IDisposable
{
    public readonly WebApplicationFactory<Program> Host;

    public IntegrationClassFixture()
    {
        Host = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(e =>
                {
                    e.AddDbContext<BananagramsDatabase>(options => options
                            .EnableSensitiveDataLogging()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString()),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    e.AddTransient<IBananagramsDatabase, BananagramsDatabase>();
                });
            });
        DatabaseSeed.SeedDatabase(Host.Services.GetService<BananagramsDatabase>());
    }

    public void Dispose()
    {
        Host?.Dispose();
    }
}