using Animal_Shelter;
using Animal_Shelter.Data;
using AnimalShelterTests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AnimalShelterTests.Fixtures;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly AnimalShelterDbContext _context;
    public AnimalShelterDbContext Context => _context;

    public CustomWebApplicationFactory(AnimalShelterDbContext context)
    {
      _context = context;

    }
    protected override void ConfigureWebHost (IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(service => service.ServiceType == typeof(AnimalShelterDbContext));
            if (descriptor != null) services.Remove(descriptor);
            services.AddSingleton(_context);
        });
    }
}
