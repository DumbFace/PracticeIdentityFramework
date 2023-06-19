using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeIdentity.Permissions
{
    public static class PermissionsAuthorize
    {
        public static class Product
        {

            public const string GroupPermission = "Product";
            public const string View = "Permissions.Product.View";
            public const string Create = "Permissions.Product.Create";
            public const string Edit = "Permissions.Product.Edit";
            public const string Delete = "Permissions.Product.Delete";
        }

        public static class Role
        {
            public const string GroupPermission = "Role";
            public const string View = "Permissions.Role.View";
            public const string Create = "Permissions.Role.Create";
            public const string Edit = "Permissions.Role.Edit";
            public const string Delete = "Permissions.Role.Delete";
        }

        public static class User
        {
            public const string GroupPermission = "User";
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Edit = "Permissions.User.Edit";
            public const string Delete = "Permissions.User.Delete";
        }

        
    }
}