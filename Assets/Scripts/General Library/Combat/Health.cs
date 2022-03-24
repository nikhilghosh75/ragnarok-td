/*
 * Manages a health value for a character
 * @Max Perraut '20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSoft.Combat
{
    // the damage type of towers
    [System.Serializable]
    public enum DamageType
    {
        Physical,
        Magical
    }

    public class Health : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The current health")]
        private int current;

        public int Current { get { return current; } }

        [Tooltip("The maximum amount of health that the player is allowed to have")]
        public int max;

        [Tooltip("Are invincibility frames enabled")]
        public bool iframesEnabled = false;

        [Tooltip("The duration of the iFrames, if iFrames are enabled")]
        public float iframesDuration = 1f;

        [Tooltip("Is the enemy immune to Magical damage?")]
        public bool isImmuneToMagicalDamage = false;

        [Tooltip("Is the enemy immune to Physical damage?")]
        public bool isImmuneToPhysicalDamage = false;

        [Tooltip("Is the enemy hidden?")]
        public bool isHidden = false;

        //Wrap all events in a struct to group them in Editor
        [System.Serializable]
        public struct HealthEvents
        {
            [System.Serializable]
            public class HealthValueEvent : UnityEvent<int> { }

            [System.Serializable]
            public class IframeDurationEvent : UnityEvent<float> { }

            [Tooltip("Invoked when a heal occurs.")]
            public UnityEvent OnHeal;

            [Tooltip("Invoked when damage occurs.")]
            public UnityEvent OnDamage;

            [Tooltip("Invoked when iframes are triggered. Passed the duration of iframes.")]
            public IframeDurationEvent OnIframes;

            [Tooltip("Invoked when health changes. It is passed the value of health after the change.")]
            public HealthValueEvent OnHealthChange;

            [Tooltip("Invoked when health reaches zero.")]
            public UnityEvent OnDeath;
        }

        [Tooltip("A struct that contains the UnityEvents")]
        public HealthEvents events;

        // The time at which the iFrames end, in Unity time
        private float iframesEnd = 0f;

        // Is the enemy dead
        private bool isDead;

        private void Start()
        {
            if (current != max)
            {
                Debug.Log("Starting at less than maximum health.");
            }
            // switch the enemy to another layer
            if (isHidden)
            {
                gameObject.layer = LayerMask.NameToLayer("HiddenEnemy");
            }
        }

        /// <summary>
        /// Adds health up to the maximum
        /// </summary>
        public void Heal(int amount)
        {
            //Assumes if being healed, entity is not dead
            isDead = false;
            current += amount;

            if (current > max) current = max;

            events.OnHealthChange.Invoke(current);
            events.OnHeal.Invoke();
        }

        /// <summary>
        /// Applies damage
        /// optionally pass in the damage type as the second parameter
        /// </summary>
        public void ApplyDamage(int amount, DamageType type = DamageType.Physical)
        {
            // do nothing if is immune to damage
            if (type == DamageType.Physical && isImmuneToPhysicalDamage)
                return;
            if (type == DamageType.Magical && isImmuneToMagicalDamage)
                return;

            // do iframes if enabled
            if (iframesEnabled)
            {
                // block damage if in iframe time
                if (Time.time < iframesEnd) return;

                // start iframes
                iframesEnd = Time.time + iframesDuration;

                events.OnIframes.Invoke(iframesDuration);
            }

            current -= amount;
            //make sure that we're not already 
            // isDead before trying to die
            if (current <= 0 && !isDead)
            {
                isDead = true;
                current = 0;
                events.OnDeath.Invoke();
            }

            events.OnHealthChange.Invoke(current);
            events.OnDamage.Invoke();
        }

        public IEnumerator ApplyDamageOverTime(int singleDamageAmount, int damageApplyTimes, float timeBetweenDamage, DamageType type = DamageType.Physical)
        {
            WaitForSeconds waitForNextDamage = new WaitForSeconds(timeBetweenDamage);

            for (int i = 0; i < damageApplyTimes; i++)
            {
                ApplyDamage(singleDamageAmount, type);
                yield return waitForNextDamage;
            }
        }
    }
}
