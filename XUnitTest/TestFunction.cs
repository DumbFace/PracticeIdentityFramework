using PracticeIdentity.Utils;
namespace XUnitTest;

public class TestFunction
{
    [Fact]
    public void Test_GeneratePermissionStatic_ShouldDisplay()
    {
        var lstPermission = Util.GeneratePermissions();
        Util.DisplayPermission(lstPermission);
    }
}