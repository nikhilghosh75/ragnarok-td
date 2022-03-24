/*
 * Controls enemy death.
 * @ Alex Kisil '19
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSoft.Combat
{
    public class EnemyDeath : MonoBehaviour
    {

        // PARAMETERS
        [SerializeField] public GameObject corpseToSpawn;
        [SerializeField] float despawnTime = 0f;

        public UnityEvent OnDeath;
        public UnityEvent OnPreDeath;



        IEnumerator Disable()
        {
            yield return new WaitForSeconds(despawnTime);

            OnDeath.Invoke();
            Destroy(gameObject);
        }

        /// <summary>
        /// Clean things up upon the enemy's death
        /// </summary>
        public void PreDisable()
        {
            OnPreDeath.Invoke();

            // Turn off collider for this enemy
            var colliders = GetComponentsInChildren<SphereCollider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            if(corpseToSpawn != null)
            {
                // Instantiate a corpse in this enemy's place
                GameObject corpse = Instantiate(corpseToSpawn, transform.position, transform.rotation) as GameObject;
            }

            // Disable this object with despawnTime seconds delay
            StartCoroutine(Disable());
        }

    }
}