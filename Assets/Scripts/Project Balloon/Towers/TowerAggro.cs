using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSoft.Math;

/*
 * A script that keeps track of the enemies in range
 * Written by Nikhil Ghosh
 */

[System.Serializable]
public class TowerAggroEvent : UnityEvent<GameObject> {  }

public class TowerAggro : MonoBehaviour
{
    [System.Serializable]
    public class TowerAggroEvents
    {
        public TowerAggroEvent OnRangeEnter;
        public TowerAggroEvent OnRangeExit;
    }

    [HideInInspector]
    public List<GameObject> enemiesInRange;

    [HideInInspector]
    public List<EnemyMovement> enemyMovementsInRange;

    public TowerAggroEvents events;
    public LayerMask enemyMask;

    Collider2D aggroTrigger;

    // Start is called before the first frame update
    void Start()
    {
        aggroTrigger = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayermaskFunctions.IsInLayerMask(enemyMask, collision.gameObject.layer))
        {
            enemiesInRange.Add(collision.gameObject);
            enemyMovementsInRange.Add(collision.GetComponent<EnemyMovement>());
            events.OnRangeEnter.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayermaskFunctions.IsInLayerMask(enemyMask, collision.gameObject.layer))
        {
            enemiesInRange.Remove(collision.gameObject);
            enemyMovementsInRange.Remove(collision.GetComponent<EnemyMovement>());
            events.OnRangeExit.Invoke(collision.gameObject);
        }
    }
}
