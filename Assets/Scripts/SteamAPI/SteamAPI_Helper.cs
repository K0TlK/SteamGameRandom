using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.SteamAPI
{
    public static class SteamAPI_Helper
    {
        private const string API_KEY = "41ADB7B4D7DB6E38E61360C6F8A81AB2";

        public static UnityWebRequest GetRequestWithKey(string url)
        {
            string urlWithApiKey = $"{url}?key={API_KEY}";
            return UnityWebRequest.Get(urlWithApiKey);
        }

        public static UnityWebRequest GetRequestWithKey(string url, Dictionary<string, string> parameters)
        {
            parameters["key"] = API_KEY;

            string fullUrl = url + "?";
            foreach (var param in parameters)
            {
                fullUrl += $"{UnityWebRequest.EscapeURL(param.Key)}={UnityWebRequest.EscapeURL(param.Value)}&";
            }
            fullUrl = fullUrl.TrimEnd('&');

            UnityWebRequest request = UnityWebRequest.Get(fullUrl);

            return request;
        }
    }
}