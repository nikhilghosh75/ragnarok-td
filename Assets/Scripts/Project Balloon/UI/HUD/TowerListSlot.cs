using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * A script representing a slot for a single tower
 * Written by Nikhil Ghosh '24
 */

public class TowerListSlot : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Text costText;

    [Header("Color")]
    public Color enoughMoneyColor;
    public Color notEnoughMoneyColor;

    Tower tower;

    [HideInInspector]
    public TowerList owningTowerList;
    TooltipTrigger tooltipTrigger;

    // Start is called before the first frame update
    private void Start()
    {
        tooltipTrigger = GetComponent<TooltipTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tower == null)
            return;

        bool canAfford = PlayerResources.Get().GetCurrency() >= tower.cost;
        costText.color = canAfford ? enoughMoneyColor : notEnoughMoneyColor;
    }

    public void SetTower(Tower newTower)
    {
        tower = newTower;

        icon.sprite = tower.icon;
        costText.text = tower.cost.ToString();
        if(tooltipTrigger == null)
            tooltipTrigger = GetComponent<TooltipTrigger>();

        tooltipTrigger.title = newTower.towerName;
        tooltipTrigger.content = newTower.towerDescription;
    }

    public void OnTowerClicked()
    {
        int currency = PlayerResources.Get().GetCurrency();
        if(currency >= tower.cost)
        {
            owningTowerList.OnTowerSelected(this, tower);
        }
    }

    public void OnTowerPointerDown()
    {
        int currency = PlayerResources.Get().GetCurrency();
        if (currency >= tower.cost)
        {
            owningTowerList.OnTowerSelected(this, tower);
        }
    }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     owningTowerList.OnTowerHoverStart(this, tower);
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     owningTowerList.OnTowerHoverEnd(this, tower);
    // }
}
