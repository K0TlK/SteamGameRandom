using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField _text;
    [SerializeField] private GameListMaker _maker;

    public void AddGameByID()
    {
        _maker.AddGame(_text.text);
    }
}
