using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that allows for physics-based knockback
 * Requires a UnityEngine.CharacterController
 * Written by Minkang Choi '?
 */

public class KnockbackReceiver : MonoBehaviour
{
    public float mass = 3.0f; // defines the character mass
    public float recoverability = 5;
    public Vector3 impact = Vector3.zero;
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2) cc.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, recoverability * Time.deltaTime);
    }
}