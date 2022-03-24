using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A list of towers in the game
 * Written by Nikhil Ghosh '24
 */

public class TowerList : MonoBehaviour
{
    public TowerCollection towerCollection;

    public GameObject towerSlotPrefab;

    public TowerDropper towerDropper;

    // public Text towerTitle;
    // public Text towerDescription;

    // Start is called before the first frame update
    void Start()
    {
        DestroyAllChildren();

        for(int i = 0; i < towerCollection.towers.Count; i++)
        {
            SpawnTowerSlot(towerCollection.towers[i]);
        }

        towerDropper.gameObject.SetActive(false);
        // towerTitle.gameObject.SetActive(false);
        // towerDescription.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTowerSelected(TowerListSlot slot, Tower tower)
    {
        towerDropper.gameObject.SetActive(true);
        towerDropper.SetTower(tower);
    }

    // public void OnTowerHoverStart(TowerListSlot slot, Tower tower)
    // {
    //     towerTitle.gameObject.SetActive(true);
    //     towerTitle.text = tower.towerName;

    //     towerDescription.gameObject.SetActive(true);
    //     towerDescription.text = tower.towerDescription;
    // }

    // public void OnTowerHoverEnd(TowerListSlot slot, Tower tower)
    // {
    //     towerTitle.gameObject.SetActive(false);
    //     towerDescription.gameObject.SetActive(false);
    // }

    void DestroyAllChildren()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void SpawnTowerSlot(Tower tower)
    {
        GameObject spawnedObject = Instantiate(towerSlotPrefab, transform);
        TowerListSlot spawnedSlot = spawnedObject.GetComponent<TowerListSlot>();
        spawnedSlot.owningTowerList = this;
        spawnedSlot.SetTower(tower);
    }
}
