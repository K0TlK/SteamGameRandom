using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Assets.Scripts.SteamAPI
{
    public static class AppDetails
    {
        public enum GameCategory
        {
            Multiplayer = 1,
            SinglePlayer = 2,
            ModsRequireHL2 = 6,
            ValveAntiCheatEnabled = 8,
            Coop = 9,
            GameDemo = 10,
            CaptionsAvailable = 13,
            CommentaryAvailable = 14,
            Stats = 15,
            IncludesSourceSDK = 16,
            IncludesLevelEditor = 17,
            PartialControllerSupport = 18,
            Mods = 19,
            MMO = 20,
            SteamAchievements = 22,
            SteamCloud = 23,
            SharedSplitScreen = 24,
            SteamLeaderboards = 25,
            CrossPlatformMultiplayer = 27,
            FullControllerSupport = 28,
            SteamTradingCards = 29,
            SteamWorkshop = 30,
            VRSupport = 31,
            SteamTurnNotifications = 32,
            NativeSteamControllerSupport = 33,
            InAppPurchases = 35,
            OnlinePvP = 36,
            SharedSplitScreenPvP = 37,
            OnlineCoop = 38,
            SharedSplitScreenCoop = 39,
            SteamVRCollectibles = 40,
            RemotePlayOnPhone = 41,
            RemotePlayOnTablet = 42,
            RemotePlayOnTV = 43,
            RemotePlayTogether = 44,
            LanPvP = 47,
            LanCoop = 48,
            PvP = 49,
            SteamWorkshopAlt = 51,
            TrackedControllerSupport = 52,
            VRSupported = 53,
            VROnly = 54,
            HDRAvailable = 61
        }

        public static UnityWebRequest AppDetailsRequest(string appID)
        {
            string url = "https://store.steampowered.com/api/appdetails?appids=" + appID;
            return UnityWebRequest.Get(url); ;
        }

        public static UnityWebRequest AppDetailsRequest(string appID, string region)
        {
            string url = "https://store.steampowered.com/api/appdetails?cc=" + region + "&appids=" + appID;
            return UnityWebRequest.Get(url); ;
        }

        public static Root ParseData(string json)
        {
            var jObject = JObject.Parse(json);

            Root root = new Root
            {
                Games = new Dictionary<string, GameResponse>()
            };

            foreach (var property in jObject.Properties())
            {
                var gameResponse = property.Value.ToObject<GameResponse>();

                root.Games.Add(property.Name, gameResponse);
            }
            
            return root;
        }

        public static GameResponse GetGameResponse(string json)
        {
            var data = ParseData(json);
            foreach (var game in data.Games.Values)
            {
                return game;
            }

            return null;
        }

        public class Root
        {
            public Dictionary<string, GameResponse> Games { get; set; }
        }

        public class GameResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public class Category
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }
        }

        public class ContentDescriptors
        {
            [JsonProperty("ids")]
            public List<object> ids { get; set; }

            [JsonProperty("notes")]
            public object notes { get; set; }
        }

        public class Data
        {
            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("steam_appid")]
            public int steam_appid { get; set; }

            [JsonProperty("required_age")]
            public int required_age { get; set; }

            [JsonProperty("is_free")]
            public bool is_free { get; set; }

            [JsonProperty("dlc")]
            public List<int> dlc { get; set; }

            [JsonProperty("detailed_description")]
            public string detailed_description { get; set; }

            [JsonProperty("about_the_game")]
            public string about_the_game { get; set; }

            [JsonProperty("short_description")]
            public string short_description { get; set; }

            [JsonProperty("supported_languages")]
            public string supported_languages { get; set; }

            [JsonProperty("reviews")]
            public string reviews { get; set; }

            [JsonProperty("header_image")]
            public string header_image { get; set; }

            [JsonProperty("capsule_image")]
            public string capsule_image { get; set; }

            [JsonProperty("capsule_imagev5")]
            public string capsule_imagev5 { get; set; }

            [JsonProperty("website")]
            public string website { get; set; }

            [JsonProperty("pc_requirements")]
            public JToken pc_requirements { get; set; }

            [JsonProperty("mac_requirements")]
            public JToken mac_requirements { get; set; }

            [JsonProperty("linux_requirements")]
            public JToken linux_requirements { get; set; }

            [JsonProperty("developers")]
            public List<string> developers { get; set; }

            [JsonProperty("publishers")]
            public List<string> publishers { get; set; }

            [JsonProperty("packages")]
            public List<int> packages { get; set; }

            [JsonProperty("package_groups")]
            public List<PackageGroup> package_groups { get; set; }

            [JsonProperty("platforms")]
            public Platforms platforms { get; set; }

            [JsonProperty("metacritic")]
            public Metacritic metacritic { get; set; }

            [JsonProperty("categories")]
            public List<Category> categories { get; set; }

            [JsonProperty("genres")]
            public List<Genre> genres { get; set; }

            [JsonProperty("screenshots")]
            public List<Screenshot> screenshots { get; set; }

            [JsonProperty("movies")]
            public List<Movie> movies { get; set; }

            [JsonProperty("recommendations")]
            public Recommendations recommendations { get; set; }

            [JsonProperty("release_date")]
            public ReleaseDate release_date { get; set; }

            [JsonProperty("support_info")]
            public SupportInfo support_info { get; set; }

            [JsonProperty("background")]
            public string background { get; set; }

            [JsonProperty("background_raw")]
            public string background_raw { get; set; }

            [JsonProperty("content_descriptors")]
            public ContentDescriptors content_descriptors { get; set; }
        }

        public class Genre
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }
        }

        public class Requirements
        {
            [JsonProperty("minimum")]
            public string minimum { get; set; }

            [JsonProperty("recommended")]
            public string recommended { get; set; }

            public Requirements()
            {
                minimum = string.Empty;
                recommended = string.Empty;
            }
        }

        public class Metacritic
        {
            [JsonProperty("score")]
            public int score { get; set; }

            [JsonProperty("url")]
            public string url { get; set; }
        }

        public class Movie
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("thumbnail")]
            public string thumbnail { get; set; }

            [JsonProperty("webm")]
            public Webm webm { get; set; }

            [JsonProperty("mp4")]
            public Mp4 mp4 { get; set; }

            [JsonProperty("highlight")]
            public bool highlight { get; set; }
        }

        public class Mp4
        {
            [JsonProperty("480")]
            public string _480 { get; set; }

            [JsonProperty("max")]
            public string max { get; set; }
        }

        public class PackageGroup
        {
            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("selection_text")]
            public string selection_text { get; set; }

            [JsonProperty("save_text")]
            public string save_text { get; set; }

            [JsonProperty("display_type")]
            public int display_type { get; set; }

            [JsonProperty("is_recurring_subscription")]
            public string is_recurring_subscription { get; set; }

            [JsonProperty("subs")]
            public List<Sub> subs { get; set; }
        }

        public class Platforms
        {
            [JsonProperty("windows")]
            public bool windows { get; set; }

            [JsonProperty("mac")]
            public bool mac { get; set; }

            [JsonProperty("linux")]
            public bool linux { get; set; }
        }

        public class Recommendations
        {
            [JsonProperty("total")]
            public int total { get; set; }
        }

        public class ReleaseDate
        {
            [JsonProperty("coming_soon")]
            public bool coming_soon { get; set; }

            [JsonProperty("date")]
            public string date { get; set; }
        }

        public class Screenshot
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("path_thumbnail")]
            public string path_thumbnail { get; set; }

            [JsonProperty("path_full")]
            public string path_full { get; set; }
        }

        public class Sub
        {
            [JsonProperty("packageid")]
            public int packageid { get; set; }

            [JsonProperty("percent_savings_text")]
            public string percent_savings_text { get; set; }

            [JsonProperty("percent_savings")]
            public int percent_savings { get; set; }

            [JsonProperty("option_text")]
            public string option_text { get; set; }

            [JsonProperty("option_description")]
            public string option_description { get; set; }

            [JsonProperty("can_get_free_license")]
            public string can_get_free_license { get; set; }

            [JsonProperty("is_free_license")]
            public bool is_free_license { get; set; }

            [JsonProperty("price_in_cents_with_discount")]
            public int price_in_cents_with_discount { get; set; }
        }

        public class SupportInfo
        {
            [JsonProperty("url")]
            public string url { get; set; }

            [JsonProperty("email")]
            public string email { get; set; }
        }

        public class Webm
        {
            [JsonProperty("480")]
            public string _480 { get; set; }

            [JsonProperty("max")]
            public string max { get; set; }
        }
    }
}