using DbSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var exeDirectory = System.IO.Path.GetDirectoryName(exePath);
        var builderAppSetting = new ConfigurationBuilder()
                                 .SetBasePath(exeDirectory)
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration config = builderAppSetting.Build();
        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();

        var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter("", LogLevel.None)))
                .EnableSensitiveDataLogging(false)
                );
                services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
                services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                services.AddHostedService<Startup>();
            })
            .UseSerilog();

        builder.RunConsoleAsync().GetAwaiter().GetResult();
    }
}