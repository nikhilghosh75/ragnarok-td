using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveButton : MonoBehaviour
{
    public void LoadGame()
    {
        TDManager.Get().StartLoadSave();
    }

    public void NewGame()
    {
        // Delete old save
        SaveSystem.Get().DeleteSave(TDManager.Get().levelName, TDManager.Get().currentSetting.settingName);
        TDManager.Get().StartGame();
    }
}
