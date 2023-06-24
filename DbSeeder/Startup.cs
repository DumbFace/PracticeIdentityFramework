using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DbSeeder
{
    public class Startup : IHostedService
    {
        readonly private IDatabaseSeeder _dbSeeder;
        readonly private IHostApplicationLifetime _hostApp;
        readonly private IConfiguration _config;
        readonly private IServiceCollection _service;
        public Startup(
            IHostApplicationLifetime appLifetime,
            IDatabaseSeeder dbSeeder
            )
        {
            _dbSeeder = dbSeeder;
            _hostApp = appLifetime;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("--> Seeding DB");
            await _dbSeeder.SeedUserAsync();
            await _dbSeeder.SeedRole();
            await _dbSeeder.SeedPermissionsRole();
            _hostApp.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("--> Stop Seeding DB");
            return Task.CompletedTask;
        }
    }
}