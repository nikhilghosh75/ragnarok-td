using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireRateUpgrade : TowerUpgrade
{
    public float fireRateMultiplier;

    public FireRateUpgrade(float newFireRateMultiplier)
    {
        fireRateMultiplier = newFireRateMultiplier;
    }

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        FireRateUpgradableComponent fireRateUpgradable = towerToUpgrade.GetComponent<FireRateUpgradableComponent>();
        if(fireRateUpgradable != null)
        {
            fireRateUpgradable.UpgradeFireRate(fireRateMultiplier);
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a FireRateUpgradable component");
        }
    }
}
