// Helpers/Settings.cs

using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EDIS.Core
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

        #region Setting Constants

	    private const string UsernameKey = "username";
	    private static readonly string UsernameDefault = string.Empty;

	    private const string PasswordKey = "password";
	    private static readonly string PasswordDefault = string.Empty;

	    private const string RememberMeKey = "remember_me";
	    private static readonly bool RememberMeDefault = false;

        private const string AccessTokenKey = "access_token_key";
	    private static readonly string TokenDefault = string.Empty;

	    private const string EstateIdKey = "estate_id_key";
	    private static readonly string EstateIdDefault = string.Empty;

	    private const string BuildingIdKey = "building_id_key";
	    private static readonly string BuildingIdDefault = string.Empty;

	    private const string ApiKey = "api_key";
	    private static readonly string ApiDefault = string.Empty;

	    private const string PrivilegesKey = "privileges_key";
	    private static readonly string PrivilegesDefault = string.Empty;

        private const string AccessTokenExpireTimeKey = "access_toke_expire_time_key";
	    private static readonly DateTime AccessTokenExpireTimeDefault = DateTime.UtcNow;

        public static string WebAppBaseURL = "http://edis-dev.electricalcertificates.co.uk";
        public static string profileFolder = "profile";

        private const string UserIdKey = "user_id_key";
        private static readonly string UserIdDefault = string.Empty;

        #endregion


        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(UserIdKey, value); }
        }

        public static string Username
	    {
	        get { return AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault); }
	        set { AppSettings.AddOrUpdateValue(UsernameKey, value); }
	    }

	    public static string Password
	    {
	        get { return AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault); }
	        set { AppSettings.AddOrUpdateValue(PasswordKey, value); }
	    }

	    public static bool RememberMe
	    {
	        get { return AppSettings.GetValueOrDefault(RememberMeKey, RememberMeDefault); }
	        set { AppSettings.AddOrUpdateValue(RememberMeKey, value); }
	    }

        public static string AccessToken
	    {
	        get { return AppSettings.GetValueOrDefault(AccessTokenKey, TokenDefault); }
	        set { AppSettings.AddOrUpdateValue(AccessTokenKey, value); }
	    }

	    public static string EstateId
	    {
	        get { return AppSettings.GetValueOrDefault(EstateIdKey, EstateIdDefault); }
	        set { AppSettings.AddOrUpdateValue(EstateIdKey, value); }
	    }

	    public static string BuildingId
	    {
	        get { return AppSettings.GetValueOrDefault(BuildingIdKey, BuildingIdDefault); }
	        set { AppSettings.AddOrUpdateValue(BuildingIdKey, value); }
	    }

	    public static string Api
	    {
	        get { return AppSettings.GetValueOrDefault(ApiKey, ApiDefault); }
	        set { AppSettings.AddOrUpdateValue(ApiKey, value); }
	    }

	    public static string Privileges
	    {
	        get { return AppSettings.GetValueOrDefault(PrivilegesKey, PrivilegesDefault); }
	        set { AppSettings.AddOrUpdateValue(PrivilegesKey, value); }
	    }

        public static DateTime AccessTokenExpireTime
	    {
	        get { return AppSettings.GetValueOrDefault(AccessTokenExpireTimeKey, DateTime.UtcNow); }
	        set { AppSettings.AddOrUpdateValue(AccessTokenExpireTimeKey, value); }
	    }

	    public static void RemoveUsername()
	    {
	        AppSettings.Remove(UsernameKey);
	    }

	    public static void RemovePassword()
	    {
	        AppSettings.Remove(PasswordKey);
	    }

	    public static void RemoveAccessToken()
	    {
	        AppSettings.Remove(AccessTokenKey);
	    }

	    public static void RemoveAccessTokenExpireTime()
	    {
	        AppSettings.Remove(AccessTokenExpireTimeKey);
	    }

    }
}