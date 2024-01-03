using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterPack
{
    public static class EventManager
    {
        public static GlobalEvent OnStartGame = new GlobalEvent();
        public static GlobalEvent OnEndGame = new GlobalEvent();

        public static void Reset()
        {
            OnStartGame.Dispose();
            OnEndGame.Dispose();
        }
    }

    public struct EndGameStatus
    {
        public bool win;
    }
}