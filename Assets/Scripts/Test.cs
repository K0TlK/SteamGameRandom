using Assets.Scripts.SteamAPI;
using Assets.Scripts.SteamAPI.IPlayerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    private const string ACCOUNT_ID = "76561198124808482";

    private List<AppDetails.Category> _categories = new List<AppDetails.Category>();
    private List<int> _ids = new List<int>();
    private GetOwnedGamesFull.Root _ownedGames = new GetOwnedGamesFull.Root();
    private int _index = 0;

    private void Awake()
    {
        WebRequestHandler.Instance.MakeRequestSimple<GetOwnedGamesFull.Root>(GetOwnedGamesFull.GetOwnedGamesRequest(ACCOUNT_ID),StartTestGames);
    }

    private void StartTestGames(GetOwnedGamesFull.Root OwnedGames)
    {
        _index = -1;
        _ownedGames = OwnedGames;
        NextGame();
    }

    private void NextGame()
    {
        if (_index + 1 < _ownedGames.Response.Games.Count)
        {
            _index++;
            Debug.Log($"{_index} / {_ownedGames.Response.GameCount}: {_ownedGames.Response.Games[_index].AppId}-{_ownedGames.Response.Games[_index].Name}");
            WebRequestHandler.Instance.MakeRequest(
                AppDetails.AppDetailsRequest(_ownedGames.Response.Games[_index].AppId.ToString(), "kz"),
                ShowResult,
                null,
                OnError);
        }
        else
        {
            string filePath = Path.Combine(Application.persistentDataPath, "SplitScreenGames.txt");

            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (int id in _ids)
                {
                    file.WriteLine(id);
                }
            }
            Debug.Log("1. Save file: " + filePath);

            filePath = Path.Combine(Application.persistentDataPath, "Categories.txt");

            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (var categorie in _categories.OrderBy(c => c.id))
                {
                    file.WriteLine($"{categorie.id} - {categorie.description}");
                }
            }
            Debug.Log("2. Save file: " + filePath);
        }
    }

    private async void OnError(string error)
    {
        if (error == "429")
        {
            await Task.Delay(TimeSpan.FromSeconds(60.0f));
            Debug.Log($"60 sec> {_index} / {_ownedGames.Response.GameCount} ({_ownedGames.Response.Games.Count}): {_ownedGames.Response.Games[_index].AppId}-{_ownedGames.Response.Games[_index].Name}");
            WebRequestHandler.Instance.MakeRequest(
                AppDetails.AppDetailsRequest(_ownedGames.Response.Games[_index].AppId.ToString(), "kz"),
                ShowResult,
                null,
                OnError);
        }
        else
        {
            Debug.LogError(error);
            NextGame();
        }
    }

    private void ShowResult(string text)
    {
        var data = AppDetails.GetGameResponse(text);

        if (data != null)
        {
            if (data.Success == true)
            {
                if (isSplitScreenGame(data))
                {
                    _ids.Add(data.Data.steam_appid);
                }
            }
            else
            {
                Debug.LogWarning(text);
            }
        }
        else
        {
            Debug.LogError(text);
        }

        NextGame();
    }

    [ContextMenu("ShowCategories")]
    public void ShowCategories()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var category in _categories.OrderBy(c => c.id))
        {
            sb.AppendLine($"{category.id} {category.description}");
        }

        Debug.Log(sb.ToString());
    }

    private bool isSplitScreenGame(AppDetails.GameResponse data)
    {
        if (data == null)
        {
            Debug.LogError("data == null | Problem in isSplitScreenGame");
            return false;
        }

        if (data.Data == null)
        {
            Debug.LogError("data.Data == null | Problem in isSplitScreenGame");
            return false;
        }

        if (data.Data.categories == null)
        {
            Debug.LogWarning("data.Data.categories == null | Problem in isSplitScreenGame");
            return false;
        }

        foreach (var categorie in data.Data.categories)
        {
            if (!_categories.Any(c => c.id == categorie.id && c.description == categorie.description))
            {
                _categories.Add(categorie);
            }
        }

        return data.Data.categories.Any(category => category.id == 39);
    }
}
