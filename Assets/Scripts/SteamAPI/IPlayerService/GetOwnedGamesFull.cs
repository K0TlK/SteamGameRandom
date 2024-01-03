using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.SteamAPI.IPlayerService
{
    public class GetOwnedGamesFull
    {
        public static UnityWebRequest GetOwnedGamesRequest(string userID)
        {
            string url = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/";
            var parameters = new Dictionary<string, string>
            {
                { "steamid", userID },
                { "format", "json" },
                { "include_appinfo", "true" },
                { "include_played_free_games", "true" }
            };
            return SteamAPI_Helper.GetRequestWithKey(url, parameters);
        }

        public class Game
        {
            [JsonProperty("appid")]
            public int AppId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("playtime_forever")]
            public int PlaytimeForever { get; set; }

            [JsonProperty("img_icon_url")]
            public string ImgIconUrl { get; set; }

            [JsonProperty("has_community_visible_stats")]
            public bool HasCommunityVisibleStats { get; set; }

            [JsonProperty("playtime_windows_forever")]
            public int PlaytimeWindowsForever { get; set; }

            [JsonProperty("playtime_mac_forever")]
            public int PlaytimeMacForever { get; set; }

            [JsonProperty("playtime_linux_forever")]
            public int PlaytimeLinuxForever { get; set; }

            [JsonProperty("rtime_last_played")]
            public long RtimeLastPlayed { get; set; }

            [JsonProperty("playtime_disconnected")]
            public int PlaytimeDisconnected { get; set; }
        }

        public class Response
        {
            [JsonProperty("game_count")]
            public int GameCount { get; set; }

            [JsonProperty("games")]
            public List<Game> Games { get; set; }
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }
}