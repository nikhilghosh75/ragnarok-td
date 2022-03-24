using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetter : MonoBehaviour
{
    public TowerAggro towerAggro;

    public GameObject GetEnemyToTarget()
    {
        // Currently targets randomly
        List<GameObject> enemiesInRange = towerAggro.enemiesInRange;
        if (enemiesInRange.Count == 0)
        {
            return null;
        }

        // loop through the entire enemyList to find the furthest one
        float maxDistanceTraveled = 0;
        GameObject target = null;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceTraveled = enemy.GetComponent<EnemyMovement>().GetCurrentPositionOnPath();
            if (distanceTraveled > maxDistanceTraveled)
            {
                maxDistanceTraveled = distanceTraveled;
                target = enemy;
            }
        }

        return target;
    }
}
