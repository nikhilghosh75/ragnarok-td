using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileHealthUpgrade : TowerUpgrade
{
    public int projectileHealthUpgrade;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        ProjectileHealthUpgradableComponent upgradable = towerToUpgrade.GetComponent<ProjectileHealthUpgradableComponent>();
        if (upgradable != null)
        {
            upgradable.UpgradeProjectileHealth(projectileHealthUpgrade);
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a ProjectileHealthUpgradable component");
        }
    }
}
