using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTowerUpgrade : TowerUpgrade
{
    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        SupportTower supportTower = towerToUpgrade.GetComponent<SupportTower>();
        if(supportTower != null)
        {
            switch (supportTower.upgradeStage)
            {
                case 0:
                    supportTower.ApplyNewUpgrade(new SeeHiddenEnemiesUpgrade());
                    break;
                case 1:
                    supportTower.ApplyNewUpgrade(new FireRateUpgrade(supportTower.secondUpgradeFireRateMultiplier));
                    break;
            }
            supportTower.upgradeStage++;
        }
    }
}
