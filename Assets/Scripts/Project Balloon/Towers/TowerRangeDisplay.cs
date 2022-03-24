using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeDisplay : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float originalRadius;

    public AK.Wwise.Event TowerSelectSFX;  
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalRadius = spriteRenderer.bounds.size.x;

        gameObject.SetActive(false);
    }

    public void ShowTowerRange(float towerRange, Vector3 position)
    {
        TowerSelectSFX.Post(gameObject);

        gameObject.SetActive(true);
        transform.position = position;

        transform.localScale = Vector3.one * (towerRange / originalRadius) * 2;
    }

    public void HideTowerRange()
    {
        gameObject.SetActive(false);
    }
}
