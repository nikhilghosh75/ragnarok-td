using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * A script that controls a tower dropper prefab
 * Note that this script may need to be redesigned
 * Written by Nikhil Ghosh '24
 */

public class TowerDropper : MonoBehaviour
{
    public LayerMask notDroppableTowerMask;
    public LayerMask droppableTrapMask;

    [HideInInspector]
    public Tower tower;

    [HideInInspector]
    public TowerList towerList;

    // direct referencing the tower image and the tower range; Edit: Rex Ma
    public Image towerImage;
    public RectTransform towerRange;

    [Header("Colors")]
    public Color placeableColor;
    public Color notPlaceableColor;
    public Color placeableRangeColor;
    public Color notPlaceaableRangeColor;

    /*
     * The amount of rects we're over that we can't drop towers on
     * Is an int instead of a bool so that we get bugs when regions overlap
     */
    public static int nonDroppableRectsOverlapping = 0;

    RectTransform rectTransform;
    Vector2 canvasScale = new Vector2(1f, 1f);
    Rect cameraRect;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GetCanvasScale();
    }

    void OnEnable()
    {
        // prevent glitches; Edit: Rex Ma
        towerRange.gameObject.SetActive(false);
        towerImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tower == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        rectTransform.position = MouseRestriction(mousePosition);

        towerRange.localScale = new Vector3(tower.range / canvasScale.x, tower.range / canvasScale.y, 1f);
        // prevent glitches; Edit: Rex Ma
        towerRange.gameObject.SetActive(true);
        towerImage.gameObject.SetActive(true);

        bool canPlaceTower = tower.isTrap ? CanPlaceTrap() : CanPlaceTower(tower.radius, tower.offset);
        towerImage.color = canPlaceTower ? placeableColor : notPlaceableColor;
        towerRange.GetComponent<Image>().color = canPlaceTower ? placeableRangeColor : notPlaceaableRangeColor;

        // check if the mouse is on the tower screen
        // if not, when release we should deselect the tower
        List<RaycastResult> results = new List<RaycastResult>();
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = mousePosition;
        EventSystem.current.RaycastAll(pointerEventData, results);
        bool onUI = false;
        foreach (var result in results)
        {
            if (result.gameObject.name == "Tower Screen")
            {
                onUI = true;
                break;
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && canPlaceTower)
        {
            // Place Tower
            Vector2 towerPosition = GetTowerWorldPosition();
            TowerManager.Get().SpawnTower(tower, towerPosition);
            gameObject.SetActive(false);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame && !canPlaceTower && !onUI)
        {
            DeselectTower();
        }

        // if right click, also deselect tower
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            DeselectTower();
        }

    }

    void DeselectTower()
    {
        tower = null;
        gameObject.SetActive(false);
    }


    public void SetTower(Tower newTower)
    {
        tower = newTower;
        var renderer = tower.prefab.GetComponentInChildren<SpriteRenderer>();
        towerImage.sprite = renderer.sprite;
        var scale = new Vector2(tower.prefab.transform.localScale.x * tower.prefab.transform.GetChild(0).localScale.x,
                                tower.prefab.transform.localScale.y * tower.prefab.transform.GetChild(0).localScale.y);
        towerImage.SetNativeSize();
        towerImage.transform.localScale = new Vector2(scale.x+0.03f, scale.y + 0.03f);
    }

    bool CanPlaceTower(float towerRadius, Vector2 towerOffset)
    {
        if (nonDroppableRectsOverlapping > 0)
            return false;

        Vector2 towerPosition = GetTowerWorldPosition();
        towerPosition += towerOffset;
        Collider2D overlappingCollider = Physics2D.OverlapCircle(towerPosition, towerRadius, notDroppableTowerMask);

        return overlappingCollider == null;
    }

    bool CanPlaceTrap()
    {
        if (nonDroppableRectsOverlapping > 0)
            return false;

        Vector2 trapPosition = GetTowerWorldPosition();
        Collider2D overlappingCollider = Physics2D.OverlapPoint(trapPosition, droppableTrapMask);

        return overlappingCollider != null;
    }

    Vector2 GetNewAnchoredPosition(Vector2 mousePosition)
    {
        return new Vector2(mousePosition.x / canvasScale.x, mousePosition.y / canvasScale.y);
    }

    Vector2 GetTowerWorldPosition()
    {
        //Check here
        //Debug.Log(RectTransformUtility.WorldToScreenPoint(null, rectTransform.position));
        Vector2 screenTowerPosition = GetNewAnchoredPosition(MouseRestriction(RectTransformUtility.WorldToScreenPoint(null, rectTransform.position)));

        return Camera.main.ScreenToWorldPoint(screenTowerPosition);
    }

    void GetCanvasScale()
    {
        Transform currentTransform = transform.parent.parent;

        while (currentTransform != null)
        {
            Canvas canvas = currentTransform.GetComponent<Canvas>();
            if (canvas != null)
            {
                /*float match = currentTransform.GetComponent<CanvasScaler>().matchWidthOrHeight;
                canvasScale = (Screen.width / currentTransform.GetComponent<CanvasScaler>().referenceResolution.x) *
                    (1 - match) + (Screen.height / currentTransform.GetComponent<CanvasScaler>().referenceResolution.y) * match;

                canvasScale = canvas.scaleFactor;*/

                cameraRect = Camera.main.pixelRect;
                canvasScale = new Vector2((float)Screen.width / cameraRect.width, (float)Screen.height / cameraRect.height);
                
            }

            currentTransform = currentTransform.parent;
        }
    }

    // Used to check if the provided mousePosition is outside of the border of screen
    // if it is, restrict it within the borders
    // Edit: Rex Ma
    /*Vector2 MouseScreenRestriction(Vector2 mousePosition)
    {
        if (mousePosition.x <= 0)
            mousePosition = new Vector2(0, mousePosition.y);
        else if (mousePosition.x >= Screen.width / canvasScale.x)
            mousePosition = new Vector2(Screen.width / canvasScale.x, mousePosition.y);

        if (mousePosition.y <= 0)
            mousePosition = new Vector2(mousePosition.x, 0);
        else if (mousePosition.y >= Screen.height / canvasScale.y)
            mousePosition = new Vector2(mousePosition.x, Screen.height / canvasScale.y);
        return mousePosition;
    }*/

    Vector2 MouseRestriction(Vector2 mousePosition)
    {
        if (mousePosition.x <= 0)
            mousePosition = new Vector2(0, mousePosition.y);
        else if (mousePosition.x >= Screen.width)
            mousePosition = new Vector2(Screen.width, mousePosition.y);

        if (mousePosition.y <= 0)
            mousePosition = new Vector2(mousePosition.x, 0);
        else if (mousePosition.y >= Screen.height)
            mousePosition = new Vector2(mousePosition.x, Screen.height);
        return mousePosition;
    }
}
