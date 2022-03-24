using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows projectiles to hit multiple target before being destroyed
 * Hitting an enemy decreases the health by 1
 * Hitting an obstacle will instantly destroy the projectile
 * Destroys projectile when health is down to 0
 * Written: Rex Ma
 */
public class ProjectileHealth : MonoBehaviour
{
    [Tooltip("Number of hits before destruction")]
    public float hitPoint = 1;
    public LayerMask layerMaskToDestroy;

    [Tooltip("The game object to be spawned on trigger. Set to null for nothing to spawn")]
    public GameObject hitParticle;

    private LayerMask layerMaskToHit;

    private void Start()
    {
        layerMaskToHit = GetComponent<WSoft.Combat.DamageOnTrigger2D>().damageLayers;
    }

    private void HitTarget()
    {
        hitPoint -= 1;
        if (hitPoint <= 0)
            Destroy(gameObject);
    }

    private void HitObstacle()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (WSoft.Math.LayermaskFunctions.IsInLayerMask(layerMaskToDestroy, collision.gameObject.layer))
        {
            if (hitParticle)
            {
                Instantiate(hitParticle, this.transform.position, Quaternion.identity);
            }
            HitObstacle();
        }
        else if (WSoft.Math.LayermaskFunctions.IsInLayerMask(layerMaskToHit, collision.gameObject.layer)) {
            if (hitParticle)
            {
                Instantiate(hitParticle, this.transform.position, Quaternion.identity);
            }
            HitTarget();
        }
    }
}
