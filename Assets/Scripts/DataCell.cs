using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataCell : MonoBehaviour
{
    [SerializeField] private Image _header;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;

    private string _headerUrl;
    private string _name;
    private string _description;
    private string _id;

    public string Header => _headerUrl;
    public string Name => _name;
    public string Description => _description;
    public string Id => _id;

    public void SetData(Sprite sprite, string headerUrl, string gameId, string name, string description = "")
    {
        _headerUrl = headerUrl;
        _id = gameId;
        _name = name;
        _description = description;

        _header.sprite = sprite;
        _nameText.text = name;
        _descriptionText.text = description;
    }
}
             