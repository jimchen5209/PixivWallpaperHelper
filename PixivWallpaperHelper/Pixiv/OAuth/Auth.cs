﻿using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using PixivWallpaperHelper.Pixiv.Utils;
using PixivWallpaperHelper.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    public class Auth
    {
        private static readonly string API = "https://oauth.secure.pixiv.net/auth/token";
        private static readonly string ClientID = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        private static readonly string ClientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";
        private static readonly string HashSecret = "28c1fdd170a5204386cb1313c7077b34f83e4aaf4aa829ce78c231e05b0bae2c";

        public static async Task<Authorize> Login(string username, string password)
        {
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "grant_type", "password" },
                { "device_token", "pixiv" },
                { "get_secure_url", "true" },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
            };

            string localTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Local).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
            string hash = MD5.MD5Code(localTime + HashSecret);

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept-Language", "en_US" } ,
                { "X-Client-Time",  localTime} ,
                { "X-Client-Hash", hash } ,
            };

            string json = await (await Request.CreateRequest(MethodType.POST, API, param, headers)).GetResponseStringAsync();
            Authorize authorize = JsonConvert.DeserializeObject<Authorize>(json);

            Data.SaveAuthData(authorize);

            return authorize;
        }
    }
}
