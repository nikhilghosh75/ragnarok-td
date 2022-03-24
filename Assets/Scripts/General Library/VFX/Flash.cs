/*
 * Flashes on a public function for the flashInterval.
 * This is intended for the white flash when enemies get hit, but is generalized enough
 * to be extended for a variety of purposes.
 * @ Matan Gottesman
 * NOT APPROVED
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.VFX
{
    public class Flash : MonoBehaviour
    {
        [Tooltip("The amount of time of the flash")]
        [SerializeField] float flashInterval;

        [Tooltip("The material to flash to. Should be a generic white material for a standardized flash.")]
        [SerializeField] Material flashMaterial;

        /// <summary>
        /// Start the flashing of the sprites for the FlashInterval
        /// </summary>
        public void FlashSprites()
        {
            StartCoroutine(StartFlash());
        }

        IEnumerator StartFlash()
        {
            List<Material> sprite_materials = new List<Material>();
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite_materials.Add(sprite.material);
                sprite.material = flashMaterial;
            }
            yield return new WaitForSeconds(flashInterval);
            int i = 0;
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.material = sprite_materials[i];
                i += 1;
            }
        }
    }
}
