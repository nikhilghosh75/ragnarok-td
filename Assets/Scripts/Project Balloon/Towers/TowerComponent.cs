using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * A component that should go on all towers. Handles tower clicking and upgrading towers
 */

[System.Serializable]
public class TowerComponent : MonoBehaviour
{
    [HideInInspector]
    public TowerActionScreen actionScreen;

    [HideInInspector]
    public Tower tower;

    Transform viewTransform = null; // child transform component with spriterenderer

    public int currentTowerUpgrade = 0;
    bool currentHighlightState = false;
    Vector2 canvasScale = Vector2.one;
    Collider2D collider2d;
    CircleCollider2D aggroRange;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();

        // Aggro is on a child with the TowerAggro component
        GameObject aggroGameObject = GetComponentInChildren<TowerAggro>().gameObject;
        aggroRange = aggroGameObject.GetComponent<CircleCollider2D>();
        aggroRange.radius = tower.range;
        GetCanvasScale();
        viewTransform = transform.GetChild(0);
    }

    void Update()
    {
        if (IsMouseOver())
        {
            if (!currentHighlightState)
            {
                // highlight color
                viewTransform.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                currentHighlightState = true;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                TowerManager.Get().ClickTower(this);
            }
        }
        else
        {
            if (currentHighlightState)
            {
                // recover original colr
                viewTransform.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                currentHighlightState = false;
            }
        }
    }

    bool IsMouseOver()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = new Vector2(mousePosition.x / canvasScale.x, mousePosition.y / canvasScale.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return collider2d.OverlapPoint(worldPosition);
    }

    void GetCanvasScale()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        var cameraRect = Camera.main.pixelRect;
        canvasScale = new Vector2(Screen.width / cameraRect.width, Screen.height / cameraRect.height);
    }

    public Vector2 GetPosition() { return collider2d.transform.position; }

    public TowerUpgrade GetNextUpgrade()
    {
        int numUpgrades = tower.towerUpgrades.Count;
        return currentTowerUpgrade < numUpgrades ? tower.towerUpgrades[currentTowerUpgrade] : null;
    }

    public void UpgradeTowerToNext()
    {
        if (currentTowerUpgrade >= tower.towerUpgrades.Count)
            return;

        UpgradeTower(tower.towerUpgrades[currentTowerUpgrade]);
        UpgradeTowerSkin();

        currentTowerUpgrade++;
        Debug.Log("upgrade");
        Debug.Log(currentTowerUpgrade);
    }

    public void UpgradeTower(string upgradeName)
    {
        foreach(TowerUpgrade upgrade in tower.towerUpgrades)
        {
            if(upgrade.name == upgradeName)
            {
                UpgradeTower(upgrade);
            }
        }
    }

    public void UpgradeTower(TowerUpgrade towerUpgrade)
    {
        towerUpgrade.ImplementUpgrade(gameObject);
    }

    public void UpgradeTowerSkin()
    {
        // upgrade tower skin by currentTowerUpgrade
        if (tower.towerSkins.Count > currentTowerUpgrade)
        {
            viewTransform.GetComponent<SpriteRenderer>().sprite = tower.towerSkins[currentTowerUpgrade];
        }
    }

    public int GetCurrentUpgradeLevel()
    {
        return currentTowerUpgrade;
    }
}
