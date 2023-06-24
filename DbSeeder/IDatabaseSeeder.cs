using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DbSeeder
{
    public interface IDatabaseSeeder
    {
        Task SeedRole();
        Task SeedUserAsync();
        Task SeedPermissionsRole();
    }
}