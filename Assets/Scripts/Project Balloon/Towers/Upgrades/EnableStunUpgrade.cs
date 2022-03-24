using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnableStunUpgrade : TowerUpgrade
{
    public float stunTime;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        StunEnableUpgradableComponent upgradable = towerToUpgrade.GetComponent<StunEnableUpgradableComponent>();
        if(upgradable != null)
        {
            upgradable.EnableStun(stunTime);
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a StunEnableUpgradable component");
        }
    }
}
