/*
 * Rotate an object towards a given object, interpolating the rotation in between
 * Note that this rotates relative to the forward vector
 * @ Ziang Wang '?, Nikhil Ghosh '23
 * NOT APPROVED
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Math
{
    public class AimAtObject : MonoBehaviour
    {
        private GameObject target;

        [Tooltip("The speed at which to be aiming at")]
        public float speed = 5;

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                Vector3 direction = (target.transform.position - transform.position);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, target.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Set the target to the specified gameobject
        /// </summary>
        /// <param name="p">The gameobject to set</param>
        public void SetTarget(GameObject p)
        {
            target = p;
        }

        /// <summary>
        /// Set the target to null
        /// </summary>
        public void ClearTarget()
        {
            target = null;
        }
    }
}
