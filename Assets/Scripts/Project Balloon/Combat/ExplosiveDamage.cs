using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSoft.Combat
{
    public class ExplosiveDamage : DamageOnTrigger2D
    {
        [Tooltip("The range of the explosive damage")]
        public float range;

        public bool stunEnabled = false;
        public float stunTime = 1f;

        public UnityEvent OnExplode;
        [SerializeField] private GameObject ExplosiveEffect;

        protected override void DoDamage(GameObject target)
        {
            // Check if collision is included in layermask
            if ((damageLayers.value & 1 << target.layer) != 0)
            {
                DoExplodeDamage(target.transform.position);
            }
        }

        private void DoExplodeDamage(Vector2 position)
        {
            OnExplode.Invoke();

            var hits = Physics2D.OverlapCircleAll(position, range);
            Instantiate(ExplosiveEffect, new Vector3(position.x, position.y, 0), Quaternion.identity);
            foreach (var hit in hits)
            {
                //Check if collision is included in layermask
                if ((damageLayers.value & 1 << hit.gameObject.layer) != 0)
                {
                    //Find health component on collided object
                    Health health = hit.GetComponent<Health>();
                    if (health)
                    {
                        health.ApplyDamage(damage, damageType);
                    }
                    EnemyMovement enemyMovement = hit.GetComponent<EnemyMovement>();
                    if(enemyMovement != null && stunEnabled)
                    {
                        enemyMovement.StunEnemy(stunTime);
                    }
                }
            }

        }
    }
}
