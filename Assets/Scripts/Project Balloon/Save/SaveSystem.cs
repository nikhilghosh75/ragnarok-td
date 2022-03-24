using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * A class designed to create save objects load them
 * Written by Minkang Choi '21
*/

public class SaveSystem : MonoBehaviour
{

    static SaveSystem instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static SaveSystem Get() { return instance; }

    private SaveData CreateSaveDataObject()
    {
        // Create new SaveData object to store current state into
        SaveData saveData = new SaveData();
        
        // Get current list of towers and save tower type, position, and number of upgrades
        List<TowerComponent> towerComponents = TowerManager.Get().GetTowers();
        for (int i = 0; i < towerComponents.Count; ++i) {
            Tower type = towerComponents[i].tower;
            int upgrades = towerComponents[i].GetCurrentUpgradeLevel();
            TowerSaveData towerSaveData = new TowerSaveData(type, towerComponents[i].GetPosition(), upgrades);
            saveData.towerSaveDataList.Add(towerSaveData);
        }

        // Save health, currency, and round number
        saveData.health = PlayerHealth.Get().GetHealth();
        saveData.currency = PlayerResources.Get().GetCurrency();
        saveData.round = EnemySpawner.Get().GetCurrentRound();

        return saveData;
    }

    public void Save()
    {
        SaveData saveData = instance.CreateSaveDataObject();
        string difficulty = TDManager.Get().currentSetting.settingName;
        string level = SceneManager.GetActiveScene().name;

        var json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        System.IO.File.WriteAllText(GenerateFileName(level, difficulty), json);

        Debug.Log("Game Saved at: " + GenerateFileName(level, difficulty));
    }
    
    public bool LoadExists(string scene, string difficulty) { return File.Exists(GenerateFileName(scene, difficulty)); }

    public bool LoadExists(string scene)
    {
        string[] saveFiles = Directory.GetFiles(Application.persistentDataPath);
        for(int i = 0; i < saveFiles.Length; i++)
        {
            if (IsSaveFileOfLevel(scene, saveFiles[i]))
            {
                Debug.Log(saveFiles[i]);
                return true;
            }
        }

        return false;
    }
    
    public void DeleteSave(string scene, string difficulty)
    {
        if (LoadExists(scene, difficulty))
        System.IO.File.Delete(GenerateFileName(scene, difficulty));
    }

    public void Load()
    {
        string scene = TDManager.Get().levelName;
        string difficulty = TDManager.Get().currentSetting.settingName;
        
        if (File.Exists(GenerateFileName(scene, difficulty)))
        {
            string json = System.IO.File.ReadAllText(GenerateFileName(scene, difficulty));
            Debug.Log(json);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            for (int i = 0; i < saveData.towerSaveDataList.Count; ++i)
            {
                GameObject spawnedTower = TowerManager.Get().SpawnTowerOnLoad(saveData.towerSaveDataList[i].tower, saveData.towerSaveDataList[i].position);
                TowerComponent spawnedTowerComponent = spawnedTower.GetComponent<TowerComponent>();

                // Upgrade tower
                spawnedTowerComponent.actionScreen.OpenActionScreen();
                for (int j = 0; j < saveData.towerSaveDataList[i].upgrades; ++j)
                {
                    spawnedTowerComponent.UpgradeTowerToNext();
                }
                spawnedTowerComponent.actionScreen.CloseActionScreen();
            }

            PlayerResources.Get().SetCurrency(saveData.currency);
            PlayerHealth.Get().SetHealth(saveData.health);
            EnemySpawner.Get().SetCurrentRound(saveData.round);

            Debug.Log("Game Loaded");
        }
    }

    private string GenerateFileName(string scene, string difficulty) {
        return Application.persistentDataPath + "/" + scene + "_" + difficulty + "_gamesave.json";
    }

    bool IsSaveFileOfLevel(string scene, string filepath)
    {
        string filename = System.IO.Path.GetFileName(filepath);
        string ext = System.IO.Path.GetExtension(filepath);

        return filename.Contains(scene) && ext == ".json";
    }
}
