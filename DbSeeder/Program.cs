using DbSeeder;
using Serilog;
using Serilog.Events;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var dbSeederService = services.GetService<IDatabaseSeeder>();

            Log.Information("-->DB Start Seeding");
            await dbSeederService.SeedUserAsync();
            await dbSeederService.SeedRole();
            await dbSeederService.SeedPermissionsRole();
            Log.Information("-->DB Stop Seeding");
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var startup = new Startup(hostContext.Configuration);
                startup.ConfigureServices(services);
            });

}