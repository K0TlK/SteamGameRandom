using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterPack
{
    public class SimpleSceneLoader : MonoBehaviour
    {
        public static void LoadScene(int index)
        {
            Debug.Log("SimpleSceneLoader: Load scene: " + index);
            SceneManager.LoadScene(index);
        }
    }
}