using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSoft.Combat;

/*
 * A class that manages dealing damage to the player and giving currency on death
 * Meant to go on all enemies, not towers
 * Written by Nikhil Ghosh '24
 */

public class EnemyData : MonoBehaviour
{
    public int currencyToGive;
    public int damageToDeal;

    // Start is called before the first frame update
    void Start()
    {
        Health health = GetComponent<Health>();
        health.events.OnDamage.AddListener(OnDamage);

        EnemyMovement movement = GetComponent<EnemyMovement>();
        movement.OnPathEnd.AddListener(OnPathEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDamage()
    {
        PlayerResources.Get().AddCurrency(currencyToGive);
    }

    void OnPathEnd()
    {
        PlayerHealth.Get().SubtractHealth(damageToDeal);
    }
}
