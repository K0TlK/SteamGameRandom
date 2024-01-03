using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadFromFile : MonoBehaviour
{
    [SerializeField] private GameListMaker _maker;

    public void ReadAndLogFileContents()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "SplitScreenGames.txt");

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string id in lines)
            {
                _maker.AddGame(id);
            }
        }
        else
        {
            Debug.LogWarning("Файл не найден: " + filePath);
        }
    }
}
