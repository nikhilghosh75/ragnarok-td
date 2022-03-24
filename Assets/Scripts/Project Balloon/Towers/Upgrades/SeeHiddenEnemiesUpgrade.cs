using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSoft.Math;

[System.Serializable]
public class SeeHiddenEnemiesUpgrade : TowerUpgrade
{
    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        TowerAggro towerAggro = towerToUpgrade.GetComponentInChildren<TowerAggro>();
        int hiddenLayer = LayerMask.NameToLayer("HiddenEnemy");
        towerAggro.enemyMask = LayermaskFunctions.AddToLayerMask(towerAggro.enemyMask, hiddenLayer);
    }
}

[System.Serializable]
public class DontSeeHiddenEnemiesUpgrade : TowerUpgrade
{
    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        TowerAggro towerAggro = towerToUpgrade.GetComponentInChildren<TowerAggro>();
        int hiddenLayer = LayerMask.NameToLayer("HiddenEnemy");
        towerAggro.enemyMask = towerAggro.enemyMask & ~(1<<hiddenLayer);
    }
}
