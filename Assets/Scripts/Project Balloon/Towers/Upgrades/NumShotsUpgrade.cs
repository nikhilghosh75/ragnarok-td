using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumShotsUpgrade : TowerUpgrade
{
    public int newNumShots;

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        AreaShooter areaShooter = towerToUpgrade.GetComponent<AreaShooter>();
        if(areaShooter != null)
        {
            areaShooter.numToShoot = newNumShots;
        }
        else
        {
            Debug.LogWarning(towerToUpgrade.name + " does not have a Area Shooter component");
        }    
    }
}
