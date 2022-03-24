using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpeedUpgrade : TowerUpgrade
{
    public float speedMultiplier;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        ProjectileSpeedUpgradableComponent upgradable = towerToUpgrade.GetComponent<ProjectileSpeedUpgradableComponent>();
        if(upgradable != null)
        {
            upgradable.UpgradeProjectileSpeed(speedMultiplier);
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a ProjectileSpeedUpgradable component");
        }
    }
}
