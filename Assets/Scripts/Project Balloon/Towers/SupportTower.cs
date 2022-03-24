using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTower : MonoBehaviour
{
    [SerializeField]
    public LayerMask towerMask;

    [Tooltip("The Range to multiply by when towers spawn")]
    public float initialRangeMultiplier;

    [Tooltip("The Fire Rate to multiply by when the towers spawn")]
    public float initialFireRateMultiplier;

    public float secondUpgradeFireRateMultiplier;

    CircleCollider2D aggroCollider;

    List<TowerUpgrade> towerUpgradesToApply = new List<TowerUpgrade>();
    List<TowerComponent> towersInRange = new List<TowerComponent>();

    [HideInInspector]
    public int upgradeStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        towerUpgradesToApply.Add(new RangeUpgrade(initialRangeMultiplier));
        towerUpgradesToApply.Add(new FireRateUpgrade(initialFireRateMultiplier));

        UpgradeAllTowersInRange();
        TowerManager.Get().OnTowerPlace.AddListener(OnTowerPlace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        // remove all the upgrades by applying countering downgrades
        int count = towerUpgradesToApply.Count;
        towerUpgradesToApply.Clear();
        if (count == 4)
        {
            ApplyNewUpgrade(new FireRateUpgrade(1 / secondUpgradeFireRateMultiplier));
        }
        if (count >= 3)
        {
            ApplyNewUpgrade(new DontSeeHiddenEnemiesUpgrade());
        }
        ApplyNewUpgrade(new FireRateUpgrade(1 / initialFireRateMultiplier));
        ApplyNewUpgrade(new RangeUpgrade(1 / initialRangeMultiplier));
    }

    public void ApplyNewUpgrade(TowerUpgrade upgradeToApply)
    {
        towerUpgradesToApply.Add(upgradeToApply);

        for(int i = 0; i < towersInRange.Count; i++)
        {
            towersInRange[i].UpgradeTower(upgradeToApply);
        }
    }

    void UpgradeAllTowersInRange()
    {
        TowerAggro aggro = GetComponentInChildren<TowerAggro>();
        aggroCollider = aggro.GetComponent<CircleCollider2D>();

        Collider2D[] results = new Collider2D[16];
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = towerMask;

        int numColliders = aggroCollider.OverlapCollider(filter, results);
        for (int i = 0; i < numColliders; i++)
        {
            TowerComponent tower = results[i].GetComponent<TowerComponent>();
            if (tower != null)
            {
                UpgradeTower(tower);
                towersInRange.Add(tower);
            }
        }
    }

    void OnTowerPlace(TowerComponent towerComponent)
    {
        if(towerComponent != null)
        {
            Vector3 position = towerComponent.transform.position;

            if(aggroCollider.OverlapPoint(position))
            {
                UpgradeTower(towerComponent);
                towersInRange.Add(towerComponent);
            }
        }
    }

    void UpgradeTower(TowerComponent towerToUpgrade)
    {
        for(int i = 0; i < towerUpgradesToApply.Count; i++)
        {
            towerToUpgrade.UpgradeTower(towerUpgradesToApply[i]);
        }
    }
}
