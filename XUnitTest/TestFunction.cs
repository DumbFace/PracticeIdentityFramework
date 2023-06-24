using Microsoft.AspNetCore.Identity;
using PracticeIdentity.Utils;
namespace XUnitTest;

public class TestFunction
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TestFunction(
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [Fact]
    public void Test_GeneratePermissionStatic_ShouldDisplay()
    {
        // var lstPermission = Util.GeneratePermissions();
        // Util.DisplayPermission(lstPermission);
    }

    [Fact]
    public void Test_GeneratePermissionStaticAsString_ShouldDisplay()
    {
        // var lstPermission = Util.GeneratePermissionsAsString();
        // foreach (string permission in lstPermission)
        // {
        //     Console.WriteLine(permission);
        // }
    }

    [Fact]
    public void Test_SeedingDb_ClaimRemove_Success()
    {

    }
}