using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DbSeeder
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration _config)
        {
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var exeDirectory = System.IO.Path.GetDirectoryName(exePath);

            var builder = new ConfigurationBuilder()
                                     .SetBasePath(exeDirectory)
                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

        }
    }
}