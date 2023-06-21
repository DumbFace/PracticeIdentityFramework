using DbSeeder;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.Enrich.FromLogContext()
.WriteTo.Async(s => s.File("Log/logs.txt"))
.CreateLogger();

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    }).RunConsoleAsync();




