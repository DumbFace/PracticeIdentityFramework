namespace DbSeeder.Constaints
{
    public class Constaint
    {
        public enum Role
        {
            SuperAdmin,
            Admin,
            User
        }

        public static string DefaultUser = "superadmin@gmail.com";
    }

}