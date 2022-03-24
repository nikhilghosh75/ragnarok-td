using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageUpgrade : TowerUpgrade
{
    public int damageToAdd;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        DamageUpgradableComponent damageUpgradable = towerToUpgrade.GetComponent<DamageUpgradableComponent>();
        if (damageUpgradable != null)
        {
            damageUpgradable.UpgradeDamage(damageToAdd);
        }
        else
        {
            Debug.LogWarning("Tower " + towerToUpgrade.name + " does not have a Damage Upgradable Component");
        }
    }
}