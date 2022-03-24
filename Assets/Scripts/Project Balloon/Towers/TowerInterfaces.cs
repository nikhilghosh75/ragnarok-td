using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A list of interfaces meant to make towers upgrades easy and modular
 * Written by Nikhil Ghosh '24
 */

/*
 * Meant for tower components to respond to when they upgrade on damage
 */

public interface DamageUpgradableComponent
{
    public abstract void UpgradeDamage(int damageUpgrade);
}

/*
 * Meant for towers that can upgrade their fire rate
 */
public interface FireRateUpgradableComponent
{
    public abstract void UpgradeFireRate(float fireRateMultiplier);
}

/*
 * Meant for towers that can upgrade their projectile speed
 */
public interface ProjectileSpeedUpgradableComponent
{
    public abstract void UpgradeProjectileSpeed(float projectileSpeedMultiplier);
}

/*
 * Meant for towers that can upgrade their projectile health
 */
public interface ProjectileHealthUpgradableComponent
{
    public abstract void UpgradeProjectileHealth(int healthUpgrade);
}

/*
 * Meant for towers that have projectiles that can stun
 */
public interface StunEnableUpgradableComponent
{
    public abstract void EnableStun(float timeToStun);
}

/*
 * Meant for towers that have projectiles that can upgrade explosive damage
 */
public interface ExplosiveUpgradableComponent
{
    public abstract void UpgradeExplosive(int damageIncrease, float radiusMultiplier);
}