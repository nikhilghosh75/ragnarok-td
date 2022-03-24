using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUpgrade
{
    public string name;
    [TextArea]
    public string description;
    public int cost;
    public int sellPriceChange;
    public Sprite sprite;

    protected void UpdateSellPrice(GameObject towerToUpgrade)
    {
        towerToUpgrade.GetComponent<TowerComponent>().actionScreen.UpdateSellAction(sellPriceChange);
    }

    public virtual void ImplementUpgrade(GameObject towerToUpgrade)
    {
        UpdateSellPrice(towerToUpgrade);
    }
}
