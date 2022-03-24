using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Regenrate Health over time
 * Health script is needed
 * Written by Rex Ma
 */
public class RegenHealth : MonoBehaviour
{
    private WSoft.Combat.Health health;
    private float timer = 0f;

    public float interval = 3;
    public int layerPerRegen = 1;

    private void Start()
    {
        health = GetComponent<WSoft.Combat.Health>();
    }

    private void Update()
    {
        if (!health)
            return;

        if (timer > interval)
        {
            timer = 0;
            health.Heal(layerPerRegen);
        }

        timer += Time.deltaTime;
    }
}
