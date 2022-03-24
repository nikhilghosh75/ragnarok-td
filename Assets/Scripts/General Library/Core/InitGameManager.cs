/* 
 * Creates game manager once at runtime.
 * @ Max Perraut '20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Core
{
    public class InitGameManager : MonoBehaviour
    {
        [Tooltip("The prefab of the Game Manager")]
        public GameObject gameManager;

        void Awake()
        {
            if (GameManager.Instance == null)
            {
                Instantiate(gameManager);
                Debug.Log("Initialized Game Manager.");
            }

            Destroy(gameObject);
        }
    }
}
