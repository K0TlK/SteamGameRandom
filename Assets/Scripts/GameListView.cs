using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameListView : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private DataCell _contentPrefab;

    private List<DataCell> _cells = new List<DataCell>();

    public void AddDataCell(Sprite sprite, string headerUrl, string gameId, string name, string description = "")
    {
        DataCell cell = Instantiate(_contentPrefab, _content);
        cell.SetData(sprite, headerUrl, gameId, name, description);
        _cells.Add(cell);
    }

    public bool CanAdd(string gameId)
    {
        foreach (var cell in _cells)
        {
            if (cell.Id == gameId)
            {
                return false;
            }
        }
        return true;
    }
}
