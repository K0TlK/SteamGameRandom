using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts.SteamAPI;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;

public class GameListMaker : MonoBehaviour
{
    [SerializeField] private GameListView _gameListView;

    private List<GameData> _gameData = new List<GameData>();

    private string _filePath = "game_data.json";

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, _filePath);
        LoadList();
    }

    public void AddGame(string gameId)
    {
        if (!_gameListView.CanAdd(gameId))
        {
            Debug.LogWarning("U have this game");
            return;
        }

        WebRequestHandler.Instance.MakeRequest(
                AppDetails.AppDetailsRequest(gameId, "kz"),
                AddData);
    }

    private void AddData(string jsonData)
    {
        var data = AppDetails.GetGameResponse(jsonData);

        if (data != null)
        {
            if (data.Success == true)
            {
                var gameData = new GameData();
                gameData.gameId = data.Data.steam_appid;
                gameData.gameName = data.Data.name;
                gameData.addTime = DateTime.Now;

                _gameData.Add(gameData);

                StartCoroutine(
                    DownloadImageAndAdd(
                        data.Data.header_image, 
                        data.Data.name, 
                        data.Data.steam_appid.ToString()));
            }
            else
            {
                Debug.LogWarning(jsonData);
            }
        }
        else
        {
            Debug.LogError(jsonData);
        }
    }

    IEnumerator DownloadImageAndAdd(string url, string gameName, string gameId)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "headers", gameId + ".jpg");

        if (File.Exists(filePath))
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            Sprite sprite = ConvertTextureToSprite(texture);
            _gameListView.AddDataCell(sprite, url, gameId, gameName, gameId);
        }
        else
        {
            using (UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return imageRequest.SendWebRequest();

                if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(imageRequest.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
                    Sprite sprite = ConvertTextureToSprite(texture);
                    _gameListView.AddDataCell(sprite, url, gameId, gameName, gameId);

                    SaveTextureToFile(texture, filePath);
                }
            }
        }
    }

    private Sprite ConvertTextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private void SaveTextureToFile(Texture2D texture, string filePath)
    {
        byte[] imageBytes = texture.EncodeToJPG();
        string directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllBytes(filePath, imageBytes);
    }

    [ContextMenu("Save List to JSON")]
    public void SaveList()
    {
        Root root = new Root
        {
            gameData = _gameData
        };

        string json = JsonConvert.SerializeObject(root, Formatting.Indented);

        string _filePath = Path.Combine(Application.persistentDataPath, "game_data.json");

        File.WriteAllText(_filePath, json);
        Debug.Log("Save List to JSON: " + _filePath);
    }

    public void LoadList()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            Root root = JsonConvert.DeserializeObject<Root>(json);

            if (root != null && root.gameData != null)
            {
                _gameData = root.gameData;
                root.gameData = root.gameData.OrderBy(game => game.gameId).ToList();

                foreach (var game in root.gameData)
                {
                    StartCoroutine(
                        DownloadImageAndAdd(
                            "https://cdn.akamai.steamstatic.com/steam/apps/" + game.gameId + "/header.jpg",
                            game.gameName,
                            game.gameId.ToString()));
                }
                Debug.Log("Список игр загружен.");
            }
            else
            {
                Debug.LogWarning("Не удалось десериализовать данные.");
            }
        }
        else
        {
            Debug.LogWarning("Файл не найден: " + _filePath);
        }
    }

    public class Root
    {
        [JsonProperty("game_data")]
        public List<GameData> gameData;
    }

    public class GameData
    {
        [JsonProperty("steam_appid")]
        public int gameId;

        [JsonProperty("name")]
        public string gameName;

        [JsonProperty("add_time")]
        public DateTime addTime;
    }
}
