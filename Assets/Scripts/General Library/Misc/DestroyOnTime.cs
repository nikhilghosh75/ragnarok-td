using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that destroys a game object after a certain time
 * Written by Minkang Choi '?
 */

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}