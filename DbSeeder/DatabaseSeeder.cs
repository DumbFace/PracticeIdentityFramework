using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DbSeeder.Constaints;
using PracticeIdentity.Utils;
using Serilog;
using System.Security.Claims;

namespace DbSeeder
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public string DefaultUser = "superadmin@gmail.com";

        public DatabaseSeeder(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public bool CheckRole()
        {
            return roleManager.Roles.All(s => s.Name == Constaint.Role.SuperAdmin.ToString());
        }

        public bool CheckUser()
        {
            return userManager.Users.All(s => s.UserName == DefaultUser);
        }

        public async Task SeedPermissionsRole()
        {
            var role = roleManager.Roles.Where(s => s.Name == Constaint.Role.SuperAdmin.ToString()).FirstOrDefault();
            var claimsRole = await roleManager.GetClaimsAsync(role);

            bool result = await ClearPermisionRole(claimsRole, role);
            if (result)
            {
                var superAdmin = roleManager.Roles.FirstOrDefault(s => s.Name == Constaint.Role.Admin.ToString());
                var permissions = Util.GeneratePermissionsAsString();

                foreach (string permission in permissions)
                {
                    await roleManager.AddClaimAsync(superAdmin, new Claim("Permission", permission));
                }
            }
        }


        public async Task<bool> ClearPermisionRole(IList<Claim> claimsRole, IdentityRole role)
        {
            bool result = false;
            if (claimsRole.Count > 0)
            {
                try
                {
                    foreach (var claim in claimsRole)
                    {
                        await roleManager.RemoveClaimAsync(role, claim);
                    }
                    result = true;
                    Log.Information("All Claims Removed");
                }
                catch (Exception ex)

                {
                    Log.Information($"Errror remove claims: {ex.Message}");
                }
            }
            return result;
        }


        public async Task SeedRole()
        {
            if (CheckRole())
            {
                await roleManager.CreateAsync(new IdentityRole(Constaint.Role.Admin.ToString()));
                Log.Information("Seeding Role");
            }
        }

        public async Task SeedUserAsync()
        {
            if (CheckUser())
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Constaint.DefaultUser,
                    Email = Constaint.DefaultUser,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "12345678");
                Log.Information("Seeding Account");
            }
        }
    }
}