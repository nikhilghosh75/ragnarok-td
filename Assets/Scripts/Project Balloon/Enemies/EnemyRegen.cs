using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegen : MonoBehaviour {

    [Tooltip("Whether regeneration will occur")]
    public bool enableRegeneration = true;
    [Tooltip("The rate at which the enemy recovers health")]
    [SerializeField] private float regenRate = 1.5f;
    [Tooltip("The amount of health the enemy heals")]
    [SerializeField] private int regenAmount = 1;
    private WSoft.Combat.Health health;

    private void Start() {
        health = GetComponent<WSoft.Combat.Health>();
        StartCoroutine(HealthRegeneration());
    }

    private IEnumerator HealthRegeneration() {
        while (true) {
            yield return new WaitForSeconds(regenRate);
            if (enableRegeneration && health.Current < health.max) {
                health.Heal(regenAmount);
            }
        }
    }

}
