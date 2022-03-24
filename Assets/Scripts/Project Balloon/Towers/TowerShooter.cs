using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSoft.Combat;
using WSoft.Math;

/*
 * A class that shoots projectiles at enemies
 * Note that it inherits a lot of interfaces due to the clunky way I mean upgrades
 */

public class TowerShooter : MonoBehaviour, DamageUpgradableComponent, FireRateUpgradableComponent, 
    ProjectileSpeedUpgradableComponent, ProjectileHealthUpgradableComponent, StunEnableUpgradableComponent,
    ExplosiveUpgradableComponent
{
    public GameObject projectile;
    public float timeBetweenFire;
    
    public Vector2 facingDirection;

    // For Upgrades
    int additionalDamage = 0;
    int additionalProjectileHealth = 0;
    float projectileSpeedMultiplier = 1;
    bool stunEnabled = false;
    float stunTime = 0;
    float explosiveRadiusMultiplier;

    TowerAggro towerAggro;
    TowerTargetter targetter;

    // Start is called before the first frame update
    void Start()
    {
        targetter = GetComponent<TowerTargetter>();
        towerAggro = GetComponentInChildren<TowerAggro>();
        InvokeRepeating("Shoot", timeBetweenFire, timeBetweenFire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        GameObject enemyToShootAt = targetter.GetEnemyToTarget();
        ShootAt(enemyToShootAt);
    }

    void ShootAt(GameObject enemy) {
        if (enemy == null)
            return;

        Vector2 direction = (enemy.transform.position - transform.position);
        CalculateRotation(direction);

        GameObject spawnedObject = Instantiate(projectile);
        spawnedObject.GetComponent<WSoft.Combat.DamageOnTrigger2D>().damageType = GetComponent<TowerComponent>().tower.damageType; // adjust the damage type of projectiles accordingly
        spawnedObject.transform.position = transform.position;
        spawnedObject.transform.GetChild(0).transform.right = direction;

        // All projectiles will have a projectile movement
        ProjectileMovement projectileMovement = spawnedObject.GetComponent<ProjectileMovement>();
        projectileMovement.direction = direction.normalized;
        projectileMovement.speed *= projectileSpeedMultiplier;

        DamageOnTrigger2D damageOnTrigger = spawnedObject.GetComponent<DamageOnTrigger2D>();
        if(damageOnTrigger != null)
        {
            damageOnTrigger.damage += additionalDamage;
        }
        ProjectileHealth projectileHealth = spawnedObject.GetComponent<ProjectileHealth>();
        if(projectileHealth != null)
        {
            projectileHealth.hitPoint += additionalProjectileHealth;
            damageOnTrigger.damageLayers = towerAggro.enemyMask;
            
        }
        ExplosiveDamage explosiveDamage = spawnedObject.GetComponent<ExplosiveDamage>();
        if(explosiveDamage != null)
        {
            explosiveDamage.range *= explosiveRadiusMultiplier;
        }
        if(stunEnabled)
        {
            if(explosiveDamage != null)
            {
                explosiveDamage.stunEnabled = true;
                explosiveDamage.stunTime = stunTime;
            }
            else
            {
                StunOnTrigger2D stunOnTrigger = spawnedObject.AddComponent<StunOnTrigger2D>();
                stunOnTrigger.stunTime = stunTime;
            }
        }
    }

    void CalculateRotation(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(facingDirection, direction);
        Vector3 eulerAngle = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(eulerAngle);
    }

    // The following functions are for upgrades

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

    public void UpgradeProjectileHealth(int healthUpgrade)
    {
        additionalProjectileHealth += healthUpgrade;
    }

    public void EnableStun(float timeToStun)
    {
        stunEnabled = true;
        stunTime = timeToStun;
    }

    public void UpgradeExplosive(int damageIncrease, float radiusMultiplier)
    {
        additionalDamage += damageIncrease;
        explosiveRadiusMultiplier *= radiusMultiplier;
    }
}
