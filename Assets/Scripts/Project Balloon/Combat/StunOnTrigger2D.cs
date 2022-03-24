using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunOnTrigger2D : MonoBehaviour
{
    public float stunTime;

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement movement = collision.GetComponent<EnemyMovement>();
        if(movement != null)
        {
            movement.StunEnemy(stunTime);
        }
    }
}
