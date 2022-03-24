using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeUpgrade : TowerUpgrade
{
    public float rangeMultiplier;

    public RangeUpgrade(float rangeMultiplier)
    {
        this.rangeMultiplier = rangeMultiplier;
    }

    public override void ImplementUpgrade(GameObject towerToUpgrade)
    {
        base.ImplementUpgrade(towerToUpgrade);
        GameObject aggroGameObject = towerToUpgrade.GetComponentInChildren<TowerAggro>().gameObject;
        CircleCollider2D aggroRange = aggroGameObject.GetComponent<CircleCollider2D>();
        aggroRange.radius *= rangeMultiplier;
    }
}
