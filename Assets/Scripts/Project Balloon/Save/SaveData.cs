using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class that holds wanted information for saves
 * Written by Minkang Choi '21
*/

[System.Serializable]
public class SaveData
{
    public List<TowerSaveData> towerSaveDataList = new List<TowerSaveData>();
    public string level = "";
    public int health = 0;
    public int currency = 0;
    public int round = 0;
}

[System.Serializable]
public class TowerSaveData
{
    public Tower tower;
    public Vector2 position;
    public int upgrades;

    public TowerSaveData(Tower _tower, Vector2 _position, int _upgrades)
    {
        this.tower = _tower;
        this.position = _position;
        this.upgrades = _upgrades;
    }
}
