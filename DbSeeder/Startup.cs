using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace DbSeeder
{
    public class Startup : IHostedService
    {
        private readonly IServiceCollection _services;

        public Startup(IServiceCollection services)
        {
            _services = services;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("---> Start Seeding!");
            Log.Information("---> Stop Seeding!");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConfigureServices(_services);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}