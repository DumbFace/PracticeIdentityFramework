using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DbSeeder;

public class DbHostedService : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<DbHostedService> _logger;
    private readonly IServiceCollection _services;
    public DbHostedService(
        ILogger<DbHostedService> logger, 
        IHostApplicationLifetime hostApplicationLifetime, 
        IConfiguration configuration,
        IServiceCollection services
        )
    {
        _services = services;
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
        _logger = logger;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Tạo mới Service Collection
        // var services = new ServiceCollection();

        // services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        // var serviceProvider = services.BuildServiceProvider();
        // var databaseedService = serviceProvider.GetService<IDatabaseSeeder>();



        Log.Information("---> Start Seeding!");
        // await databaseedService.SeedUserAsync();
        // await databaseedService.SeedRole();
        // await databaseedService.SeedPermissionsRole();
        Log.Information("---> Stop Seeding!");

        _hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
