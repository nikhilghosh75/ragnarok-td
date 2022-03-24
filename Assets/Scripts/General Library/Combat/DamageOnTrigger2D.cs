/* 
 * Applies damage to objects on trigger collision, use a layermask
 * @ Max Perraut '20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSoft.Combat
{
    public class DamageOnTrigger2D : MonoBehaviour
    {
        [Tooltip("The amount of damage that should be inflicted")]
        public int damage;

        [HideInInspector]
        public DamageType damageType;

        [Tooltip("Only damages objects on these layers.")]
        public LayerMask damageLayers;

        [Tooltip("Should the trigger damage when OnTriggerStay2D is called")]
        public bool damageOnStay = true;

        // This is for Crystal and isn't great coding practice, 
        // but this is the last day of the project so oh well
        public UnityEvent OnDamageRejected;

        /// <summary>
        /// On a trigger enter, find a health component on the specified object and damage.
        /// </summary>
        /// <param name="target">The GameObject to attempt to damage</param>
        protected virtual void DoDamage(GameObject target)
        {
            //Check if collision is included in layermask
            if ((damageLayers.value & 1 << target.layer) != 0)
            {
                //Find health component on collided object
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    health.ApplyDamage(damage, damageType);
                }
            }
            else
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    OnDamageRejected.Invoke();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DoDamage(collision.gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(damageOnStay)
            {
                DoDamage(collision.gameObject);
            }
        }
    }
}

