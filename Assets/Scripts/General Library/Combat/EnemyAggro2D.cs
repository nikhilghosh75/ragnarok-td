/* 
 * Handles when enemies enter aggro and the resulting actions taken in 2D.
 * This is used for activating the enemies.
 * @ Michael Tilbury (zafuru#8634)
 * NOT APPROVED
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSoft.Combat
{
    public class EnemyAggro2D : MonoBehaviour
    {
        [Header("Enemy Aggro Trigger")]

        [Tooltip("The enemy that the Aggro Range belongs to.")]
        [SerializeField] private GameObject enemyToFollow;

        [Tooltip("The radius of the circle collider")]
        private float radius;

        [Tooltip("Should the enemy follow up with a raycast when the player enters the trigger")]
        [SerializeField] bool raycastLineOfSight = false;

        [Tooltip("The layermask to raycast against")]
        [SerializeField] LayerMask raycastLayerMask;

        [Tooltip("Is the player in the collider but not found. Only applies if RaycastLineOfSIght is true")]
        private bool isSearching = false;

        private GameObject target;

        [Space(10)]

        [Header("Enemy Aggro Events")]
        // Invoked when player enters aggro range on this enemy.
        public UnityEvent OnAggro;

        // Invoked when player exits aggro range on this enemy.
        public UnityEvent OnDeaggro;

        private void Start()
        {
            // Get reference to enemy's CircleCollider2D
            CircleCollider2D collider = GetComponent<CircleCollider2D>();

            // Set the enemy's circle collider to specified aggroRadius
            collider.isTrigger = true;
            radius = collider.radius;
        }

        private void Update()
        {
            transform.position = enemyToFollow.transform.position;
        }

        /// <summary>
        /// Gets the target of the Aggro
        /// </summary>
        /// <returns>The target of the aggro (Usually the player)</returns>
        public GameObject GetTarget()
        {
            return target;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Invoke OnAggro when the player enters the enemy's trigger or enter search mode
            if (other.gameObject.CompareTag("Player"))
            {

                if (raycastLineOfSight)
                {
                    isSearching = true;
                }
                else
                {
                    target = other.gameObject;
                    OnAggro.Invoke();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // Raycast until enemy has line of sight on the player
            if (raycastLineOfSight && isSearching)
            {
                if (other.gameObject.CompareTag("Player") && HasLineOfSight(other.gameObject))
                {
                    OnAggro.Invoke();
                    isSearching = false;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Invoke OnDeaggro when the player exits the enemy's trigger 
            if (other.gameObject.CompareTag("Player"))
            {
                if (!raycastLineOfSight)
                {
                    OnDeaggro.Invoke();
                }
                else if (raycastLineOfSight && !isSearching)
                {
                    OnDeaggro.Invoke();
                }
            }
        }

        /// <summary>
        /// This raycasts to the target position up to the aggro radius and uses the passed in layermask
        /// </summary>
        /// <param name="target"> The target to raycast for</param>
        /// <returns>Was a line of sight found</returns>
        private bool HasLineOfSight(GameObject target)
        {
            // Get direction and raycast to aggro radius
            Vector2 direction = (target.transform.position - this.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, radius, raycastLayerMask);

            // Check for raycast and that hit is player
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                target = hit.collider.gameObject;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}