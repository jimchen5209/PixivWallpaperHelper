﻿using System.Collections.Generic;
using System.Configuration;
using PixivWallpaperHelper.Pixiv.Objects;

namespace PixivWallpaperHelper.Utils
{
    public class Data
    {
        public static void SaveAuthData(Authorize authorize = null)
        {
            Properties.Auth.Default.KEY_PIXIV_ACCESS_TOKEN = authorize == null ? "" : authorize.AccessToken;
            Properties.Auth.Default.KEY_PIXIV_REFRESH_TOKEN = authorize == null ? "" : authorize.RefreshToken;
            Properties.Auth.Default.KEY_PIXIV_USER_ID = (long)(authorize == null ? 0 : authorize.User.Id);
            Properties.Auth.Default.KEY_PIXIV_USER_USERNAME = authorize == null ? "" : authorize.User.Account;
            Properties.Auth.Default.KEY_PIXIV_USER_NAME = authorize == null ? "" : authorize.User.Name;
            Properties.Auth.Default.KEY_PIXIV_USER_IMG = authorize == null ? "" : authorize.User.ProfileImageUrls.Px170x170;
            Properties.Auth.Default.KEY_PIXIV_USER_LOGIN = authorize != null;
            Properties.Auth.Default.Save();
        }

        public static void ClearAuthData()
        {
            SaveAuthData();
        }

        public static void SaveSettingsData(Dictionary<string, dynamic> settingValue)
        {
            foreach (var results in settingValue)
            {
                Properties.Settings.Default[results.Key] = results.Value;
            }
            Properties.Settings.Default.Save();
        }

        public static Dictionary<string, dynamic> GetSettingsData()
        {
            Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>();

            foreach (SettingsProperty results in Properties.Settings.Default.Properties)
            {
                settings.Add(results.Name, Properties.Settings.Default[results.Name]);
            }

            return settings;
        }
    }
}
