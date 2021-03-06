﻿using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public class Recommend
    {
        public static async Task<List<Illust>> GetRecommend(string accessToken)
        {
            string API = "https://app-api.pixiv.net/v1/illust/recommended";
            List<Illust> list = new List<Illust>();

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            do
            {
                string json = await (await Request.CreateRequest(MethodType.GET, API, null, headers)).GetResponseStringAsync();
                IllustList ranking = JsonConvert.DeserializeObject<IllustList>(json);

                if (ranking.NextUrl != null) { API = ranking.NextUrl; } else { break; };

                list.AddRange(ranking.Illusts);
            } while (list.Count < Properties.Settings.Default.countNum);

            return list;
        }
    }
}
