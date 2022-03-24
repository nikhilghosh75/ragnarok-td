using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A screen that sets up the actions that a player can do to a tower
 * Currently just contains the Sell action and uses a major overhaul
 * Written by Nikhil Ghosh '24
 */

public class TowerActionScreen : MonoBehaviour
{
    public GameObject towerActionPrefab;
    public Transform towerActionContent;
    [SerializeField] private TowerActionSlot upgradeSlot1;
    [SerializeField] private TowerActionSlot upgradeSlot2;
    [SerializeField] private Text towerTitle;
    public Image towerImage;


    [SerializeField] private Text sellCostText;
    private int sellMoney = 0;

    public AK.Wwise.Event TowerSoldSFX;
    public AK.Wwise.Event TowerUpgradedSFX;

    [Header("Sprites")]
    public Sprite moneySprite;

    NonDroppableRect nonDroppableRect;

    Tower currentTower;
    TowerComponent currentTowerComponent;

    List<TowerActionSlot> actionSlots; // keeps all the slots

    private void Awake()
    {
        actionSlots = new List<TowerActionSlot>();
    }
    void Start()
    {
        nonDroppableRect = GetComponent<NonDroppableRect>();
    }

    public void SetTowerActions(Tower tower, TowerComponent towerComponent) {
        currentTower = tower;
        currentTowerComponent = towerComponent;
        SetTowerPortrait();
        towerTitle.text = tower.towerName;
        AddTowerUpgrades(tower);

        // Update the tower sell cost text
        float sellRatio = TDManager.Get() != null ? TDManager.Get().currentSetting.sellRatio : 0.8f;
        sellMoney = (int)(currentTower.cost * sellRatio);
        sellCostText.text = "$" + sellMoney.ToString();
    }

    private void SetTowerPortrait() {
        // By default, the set icon
        Sprite portrait = currentTower.icon;

        if (currentTowerComponent.currentTowerUpgrade != 0 &&
            currentTowerComponent.currentTowerUpgrade - 1 < currentTower.towerPortraits.Count) {
            portrait = currentTower.towerPortraits[currentTowerComponent.currentTowerUpgrade - 1];
        }

        towerImage.sprite = portrait;
    }

    public void SellTower()
    {
        TowerSoldSFX.Post(gameObject);
        Destroy(currentTowerComponent.gameObject);
        currentTower = null;
        currentTowerComponent = null;
        CloseActionScreen();
        PlayerResources.Get().AddCurrency(sellMoney);
    }

    public void AttemptAction(string actionName, int moneyChange)
    {
        int newPlayerMoney = PlayerResources.Get().GetCurrency() + moneyChange;
        if (newPlayerMoney < 0)
            return;

        if (actionName == "Sell")
        {
            TowerSoldSFX.Post(gameObject);

            Destroy(currentTowerComponent.gameObject);
            currentTower = null;
            currentTowerComponent = null;
            CloseActionScreen();
        }
        else
        {
            currentTowerComponent.UpgradeTowerToNext();
            SetTowerPortrait();
            TowerUpgradedSFX.Post(gameObject);
        }

        PlayerResources.Get().AddCurrency(moneyChange);
        SetTowerActions(currentTower, currentTowerComponent);
    }

    void AddSellAction(Tower tower)
    {
        GameObject spawnedPrefab = Instantiate(towerActionPrefab, towerActionContent);
        TowerActionSlot spawnedSlot = spawnedPrefab.GetComponent<TowerActionSlot>();
        float sellRatio = TDManager.Get() != null ? TDManager.Get().currentSetting.sellRatio : 0.8f;
        int sellMoney = (int)(tower.cost * sellRatio);
        spawnedSlot.owningScreen = this;
        spawnedSlot.SetSlot(moneySprite, "Sell", sellMoney);
        actionSlots.Add(spawnedSlot);
    }

    // updates the sell money
    public void UpdateSellAction(int moneyChange)
    {
        /*
        foreach (var action in actionSlots)
        {
            if (action.titleText.text == "Sell")
            {
                action.UpdateSlotCost(moneyChange);
            }
        }
        */
    }

    public void OpenActionScreen()
    {
        gameObject.SetActive(true);
    }

    public void CloseActionScreen()
    {
        gameObject.SetActive(false);
        TowerManager.Get().rangeDisplay.HideTowerRange();
    }

    void AddTowerUpgrades(Tower tower) {

        List<TowerUpgrade> upgrades = tower.towerUpgrades;
        if (upgrades.Count != 2) {
            Debug.LogWarning("This tower has the incorrect number of upgrades.");
        }
        // hard coded, but fine for the scope of this project as each tower has a set 2 upgrades
        upgradeSlot1.owningScreen = this;
        upgradeSlot1.SetSlot(upgrades[0].sprite, upgrades[0].name, -upgrades[0].cost, upgrades[0].description); ;
        upgradeSlot2.owningScreen = this;
        upgradeSlot2.SetSlot(upgrades[1].sprite, upgrades[1].name, -upgrades[1].cost, upgrades[1].description);

    }

    private void Update() {
        // This is a little overcomplicated, but it works :P
        if (currentTowerComponent.currentTowerUpgrade == 0) {
            upgradeSlot1.purchased = false;
            upgradeSlot2.purchased = false;
            upgradeSlot1.unlocked = true;
            upgradeSlot2.unlocked = false;
        }
        else if (currentTowerComponent.currentTowerUpgrade == 1) {
            upgradeSlot1.purchased = true;
            upgradeSlot2.purchased = false;
            upgradeSlot1.unlocked = true;
            upgradeSlot2.unlocked = true;
        }
        else if (currentTowerComponent.currentTowerUpgrade == 2) {
            upgradeSlot1.purchased = true;
            upgradeSlot2.purchased = true;
            upgradeSlot1.unlocked = true;
            upgradeSlot2.unlocked = true;
        }
    }
}