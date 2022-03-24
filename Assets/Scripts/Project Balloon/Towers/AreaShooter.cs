using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSoft.Combat;

public class AreaShooter : MonoBehaviour, DamageUpgradableComponent, FireRateUpgradableComponent, ProjectileSpeedUpgradableComponent
{
    public int numToShoot;
    public float timeBetweenFire;
    public float distanceToSpawn;
    public bool restrictToRange = true;
    public GameObject projectile;

    int additionalDamage = 0;
    float projectileSpeedMultiplier = 1;

    TowerTargetter targetter;
    TowerComponent towerComponent;

    // Start is called before the first frame update
    void Start()
    {
        targetter = GetComponent<TowerTargetter>();
        towerComponent = GetComponent<TowerComponent>();

        InvokeRepeating("Shoot", timeBetweenFire, timeBetweenFire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        // If there are no enemies in range, don't shoot
        if (targetter.towerAggro.enemiesInRange.Count == 0)
            return;

        float angleBetweenShots = 360f / numToShoot;
        for(int i = 0; i < numToShoot; i++)
        {
            float angle = angleBetweenShots * i * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject spawnedObject = Instantiate(projectile);
            spawnedObject.transform.position = transform.position + (Vector3)direction * distanceToSpawn;
            spawnedObject.transform.GetChild(0).transform.right = direction;
            spawnedObject.GetComponent<WSoft.Combat.DamageOnTrigger2D>().damageType = GetComponent<TowerComponent>().tower.damageType; // adjust the damage type of projectiles accordingly
            ProjectileMovement projectileMovement = spawnedObject.GetComponent<ProjectileMovement>();
            projectileMovement.direction = direction;
            projectileMovement.speed *= projectileSpeedMultiplier;
            DamageOnTrigger2D damageOnTrigger = spawnedObject.GetComponent<DamageOnTrigger2D>();
            damageOnTrigger.damage += additionalDamage;

            if (restrictToRange)
            {
                float timeUntilDestroy = towerComponent.tower.range / projectileMovement.speed;
                Destroy(spawnedObject, timeUntilDestroy);
            }    
        }
    }

    public void UpgradeDamage(int damageUpgrade)
    {
        additionalDamage += damageUpgrade;
    }

    public void UpgradeFireRate(float fireRateMultiplier)
    {
        timeBetweenFire /= fireRateMultiplier;
        CancelInvoke();
        InvokeRepeating("Shoot", timeBetweenFire, timeBetweenFire);
    }

    public void UpgradeProjectileSpeed(float projectileSpeedUpgradeMultiplier)
    {
        projectileSpeedMultiplier *= projectileSpeedUpgradeMultiplier;
    }
}
