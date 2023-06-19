using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PracticeIdentity.Models;
using PracticeIdentity.Permissions;
namespace PracticeIdentity.Utils
{
    public static class Util
    {
        public static void DisplayPermission(List<PermissionModel> permissionModels)
        {
            foreach (var permission in permissionModels)
            {
                Console.WriteLine($"Name: {permission.GroupPermission}");
                Console.WriteLine($"View: {permission.View}");
                Console.WriteLine($"Create: {permission.Create}");
                Console.WriteLine($"Edit: {permission.Edit}");
                Console.WriteLine($"Delete: {permission.Delete}");
            }
        }

        public static List<PermissionModel> GeneratePermissions()
        {
            List<PermissionModel> permissions = new List<PermissionModel>();
            var parentType = typeof(PermissionsAuthorize);

            // Get all nested types (classes)
            foreach (var type in parentType.GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
            {
                PermissionModel permissionModel = new PermissionModel();
                // Get all public constant fields in the nested class
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                {
                    if (field.IsLiteral && !field.IsInitOnly)
                    {

                        if (field.Name == "GroupPermission")
                        {
                            permissionModel.GroupPermission = field.GetValue(null).ToString();
                        }
                        else if (field.Name == "View")
                        {
                            permissionModel.View = field.GetValue(null).ToString();
                        }
                        else if (field.Name == "Create")
                        {
                            permissionModel.Create = field.GetValue(null).ToString();
                        }
                        else if (field.Name == "Edit")
                        {
                            permissionModel.Edit = field.GetValue(null).ToString();
                        }
                        else if (field.Name == "Delete")
                        {
                            permissionModel.Delete = field.GetValue(null).ToString();
                        }

                    }
                }
                permissions.Add(permissionModel);
            }
            return permissions;
        }
    }
}