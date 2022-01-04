using Web.Models.Helper;

public partial class Constants
{
    public partial class Account
    {
        public const string ROLE_ADMIN = "RoleAdmin";
        public const string ROLE_EDITOR = "RoleEditor";

        // DefaultPassword
        public static string DefaultPassword
        {
            get
            {
                return SettingsConfigHelper.AppSetting("AccessControl:DefaultPassword");
            }
        }

        // Admin
        public static string AdminUsername
        {
            get
            {
                return SettingsConfigHelper.AppSetting("AccessControl:Admin:UserName");
            }
        }

        public static string AdminEmail
        {
            get
            {
                return SettingsConfigHelper.AppSetting("AccessControl:Admin:Email");
            }
        }

        // Editor
        public static string EditorUsername
        {
            get
            {
                return SettingsConfigHelper.AppSetting("AccessControl:Editor:UserName");
            }
        }

        public static string EditorEmail
        {
            get
            {
                return SettingsConfigHelper.AppSetting("AccessControl:Editor:Email");
            }
        }
    }
}