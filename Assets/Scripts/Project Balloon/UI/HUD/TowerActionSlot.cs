using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WSoft.UI;

/*
 * A slot that displays an action a tower can do
 * Written by Nikhil Ghosh '24
 * Modified by Andrew Zhou '22
 */

public class TowerActionSlot : MonoBehaviour
{
    [HideInInspector]
    public TowerActionScreen owningScreen;

    public Image image;
    public Text titleText;
    public Text costText;

    int actionMoneyChange;
    public bool purchased = false;
    public bool unlocked = false;

    TooltipTrigger tooltipTrigger;

    private void Start()
    {
        tooltipTrigger = GetComponent<TooltipTrigger>();
    }

    /*
     * This function is subject to immense change.
     * I don't like the fact I have to hardcode "Sell Tower" as an exception
     * Or get the absolute value for prices.
     * Expect a change as the upgrades interface changes soon.
     */
    private void Update() {
        int currency = PlayerResources.Get().GetCurrency();
        int cost = Mathf.Abs(int.Parse(costText.text));

        if (currency < cost && !titleText.text.Contains("Sell")) {
            Deactivate();
        }
        if (purchased || !unlocked) {
            Deactivate();
        }
        else if (!purchased && unlocked) {
            Activate();
        }
    }

    public void SetSlot(Sprite sprite, string title, int moneyChange, string description = "")
    {
        actionMoneyChange = moneyChange;

        image.sprite = sprite;
        titleText.text = title;

        if (moneyChange > 0)
            costText.text = "+" + moneyChange.ToString();
        else
            costText.text = moneyChange.ToString();

        if(tooltipTrigger == null)
            tooltipTrigger = GetComponent<TooltipTrigger>();

        tooltipTrigger.title = title;
        tooltipTrigger.content = description;
    }

    public void UpdateSlotCost(int moneyAdd)
    {
        actionMoneyChange = int.Parse(costText.text) + moneyAdd;
        if (!costText)
            return;
        if (actionMoneyChange > 0)
            costText.text = "+" + actionMoneyChange.ToString();
        else
            costText.text = actionMoneyChange.ToString();
    }

    public void DoAction()
    {
        owningScreen.AttemptAction(titleText.text, actionMoneyChange);
    }

    public void Deactivate()
    {
        this.GetComponent<Button>().interactable = false;
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        titleText.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        costText.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
    }

    public void Activate()
    {
        this.GetComponent<Button>().interactable = true;
        image.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        titleText.color = Color.white;
        costText.color = new Color(0.6f, 0.6f, 0.6f);
    }
}
