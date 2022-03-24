using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtons : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Get().Save();
    }

    public void LoadGame(string sceneName)
    {
        
    }
}
