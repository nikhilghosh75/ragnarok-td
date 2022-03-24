using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * A class that manages the towers. Should be put on the towers GameObject in the scene
 * Written by Nikhil Ghosh
 */

public class TowerPlaceEvent : UnityEvent<TowerComponent> { }

public class TowerManager : MonoBehaviour
{
    static TowerManager instance;

    public TowerActionScreen actionScreen;

    public TowerRangeDisplay rangeDisplay;

    public AK.Wwise.Event TowerPlacedSound;

    public TowerPlaceEvent OnTowerPlace;
    public UnityEvent OnTowerClick;
    public UnityEvent OnTowerUnclick;

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

    private void Start() {
        // Creating new OnTowerPlace event
        // Was throwing a NullReferenceException previously
        if (OnTowerPlace == null) {
            OnTowerPlace = new TowerPlaceEvent();
        }
    }

    public static TowerManager Get() { return instance; }

    public void SpawnTower(Tower tower, Vector2 position)
    {
        // If can't afford, don't spawn
        if(PlayerResources.Get().GetCurrency() < tower.cost)
        {
            return;
        }

        GameObject spawnedTower = Instantiate(tower.prefab, transform);
        spawnedTower.transform.position = position;
        TowerPlacedSound.Post(gameObject);

        if(!tower.isTrap)
        {
            TowerComponent towerComponent = spawnedTower.GetComponent<TowerComponent>();
            if(towerComponent != null)
            {
                towerComponent.actionScreen = actionScreen;
                towerComponent.tower = tower;
                OnTowerPlace.Invoke(towerComponent);
            }
            else
            {
                Debug.LogWarning("Tower " + spawnedTower.name + " does not have a Tower Component");
            }
        }

        PlayerResources.Get().AddCurrency(-tower.cost);
    }

    public GameObject SpawnTowerOnLoad(Tower tower, Vector2 position)
    {
        GameObject spawnedTower = Instantiate(tower.prefab, transform);
        spawnedTower.transform.position = position;

        if(!tower.isTrap)
        {
            TowerComponent towerComponent = spawnedTower.GetComponent<TowerComponent>();
            if(towerComponent != null)
            {
                towerComponent.actionScreen = actionScreen;
                towerComponent.tower = tower;
                OnTowerPlace.Invoke(towerComponent);
            }
            else
            {
                Debug.LogWarning("Tower " + spawnedTower.name + " does not have a Tower Component");
            }
        }

        return spawnedTower;
    }

    public List<TowerComponent> GetTowers()
    {
        List<TowerComponent> towerComponents = new List<TowerComponent>();

        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            TowerComponent towerComponent = child.GetComponent<TowerComponent>();
            if (towerComponent != null)
                towerComponents.Add(towerComponent);
        }

        return towerComponents;
    }
    
    public void ClickTower(TowerComponent towerComponent)
    {
        actionScreen.gameObject.SetActive(true);
        actionScreen.SetTowerActions(towerComponent.tower, towerComponent);

        float actualRange = towerComponent.tower.range * towerComponent.transform.localScale.x;
        rangeDisplay.ShowTowerRange(actualRange, towerComponent.transform.position);
    }
}
