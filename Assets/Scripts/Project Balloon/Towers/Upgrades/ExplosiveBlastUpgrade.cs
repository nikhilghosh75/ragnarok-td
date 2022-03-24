using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExplosiveBlastUpgrade : TowerUpgrade
{
    public int damageIncrease;
    public float radiusMultiplier;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        ExplosiveUpgradableComponent upgradable = towerToUpgrade.GetComponent<ExplosiveUpgradableComponent>();
        if (upgradable != null)
        {
            upgradable.UpgradeExplosive(damageIncrease, radiusMultiplier);
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a ExplosiveUpgradable component");
        }
    }
}
