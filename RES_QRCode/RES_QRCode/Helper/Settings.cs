using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace RES_QRCode.Helper
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string Role
        {
            get
            {
                return AppSettings.GetValueOrDefault("Role", "User");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Role", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

    }
}
