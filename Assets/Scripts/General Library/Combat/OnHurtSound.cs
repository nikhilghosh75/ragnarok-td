/*
 * Play Sound when the enemy is hurt
 * @ Ziang Wang
 * NOT APPROVED
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Combat
{
    public class OnHurtSound : MonoBehaviour
    {
        // Start is called before the first frame update
        Health health;

        [Tooltip("The sound to play")]
        public WSoft.Audio.AudioEvent audioEvent;

        void Start()
        {
            health.GetComponent<Health>();
            health.events.OnDamage.AddListener(PlaySound);
        }

        /// <summary>
        /// Play the specified AudioEvent
        /// </summary>
        void PlaySound()
        {
            audioEvent.PlayAudio(this.gameObject);
        }
    }
}
