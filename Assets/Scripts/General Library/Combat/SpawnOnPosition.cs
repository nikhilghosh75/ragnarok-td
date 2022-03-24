/*
 * This script spawns a gameobject at a position
 * @ Evan Brisita '18
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Combat
{
    public class SpawnOnPosition : MonoBehaviour
    {
        [SerializeField] GameObject spawn;

        public void Spawn(Transform transform)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }

        public void Spawn(Vector3 position)
        {
            Instantiate(spawn, position, Quaternion.identity);
        }
    }
}

