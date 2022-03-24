using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSoft.Combat;

/*
 * A component set for enemies (e.g. Bulk Enemy)
 * Pump out a series of sub-enemies on the place where enemy dies on its path
 */

public class EnemySpawnOnDeath : MonoBehaviour
{
    [System.Serializable]
    public struct EnemySpawnData
    {
        public GameObject prefab;
        public int numToSpawn;
    }

    public EnemySpawnData enemyToSpawn;

    [Tooltip("Distance between generated enemies in each wave")]
    public float distanceBetweenEnemies;

    [Header("Audio")]
    public AK.Wwise.Event EnemyDeathSFX;
    
    Path path;

    void Start()
    {
        Health health = GetComponent<Health>();
        health.events.OnDeath.AddListener(OnDeath);
    }

    void OnDeath()
    {
        //Play EnemyDeath SFX when the enemy dies 
        EnemyDeathSFX.Post(gameObject);
               
        // Get original enemy path
        EnemyMovement movement = GetComponent<EnemyMovement>();
        path = movement.path;

        // Get enemy death position
        Vector3 deathPosition = transform.position;

        // Generate pre-determined enemies
        GenerateEnemies(deathPosition, movement.currentPathPoint);
    }

    void GenerateEnemies(Vector2 deathPosition, int initPathPoint)
    {
        Transform enemyTransform = transform.parent;

        int currentPathPoint = initPathPoint;
        Vector2 nextPosition = deathPosition;

        for (int i = 0; i < enemyToSpawn.numToSpawn; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemyToSpawn.prefab, enemyTransform);

            spawnedEnemy.transform.position = nextPosition;

            // Setup next position & judge path point
            Vector2 distanceToLastPathPoint = (path.points[currentPathPoint] - nextPosition);
            Vector2 direction = distanceToLastPathPoint.normalized;

            float sqrDistToLastPoint = Vector2.SqrMagnitude(distanceToLastPathPoint);
            if (currentPathPoint != 0 && distanceBetweenEnemies * distanceBetweenEnemies > sqrDistToLastPoint)
            {
                nextPosition = path.points[currentPathPoint];
                currentPathPoint--;
            }
            else
            {
                nextPosition += direction * distanceBetweenEnemies;
            }

            EnemyMovement movement = spawnedEnemy.GetComponent<EnemyMovement>();
            movement.currentPathPoint = currentPathPoint;
            movement.path = path;
        }
    }
}
