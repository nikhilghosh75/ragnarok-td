using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Combat
{
    public class DamageOverTimeOnTrigger2D : DamageOnTrigger2D
    {
        [Tooltip("The amount of damage applied over a period of time")]
        public int singleDamageOverTime;

        [Tooltip("The damage applying times over a period of time")]
        public int damageApplyTimes;

        [Tooltip("The time difference between each damage over a period of time")]
        public float timeBetweenDamage;

        protected override void DoDamage(GameObject target)
        {
            // initial damage
            base.DoDamage(target);

            // damage occuring during a period of time
            if ((damageLayers.value & 1 << target.layer) != 0)
            {
                //Find health component on collided object
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    health.ApplyDamageOverTime(singleDamageOverTime, damageApplyTimes, timeBetweenDamage, damageType);
                }
            }
        }
    }
}