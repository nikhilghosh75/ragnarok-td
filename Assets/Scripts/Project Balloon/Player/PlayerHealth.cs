using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * A script that controls the health of a stage
 * Written by Nikhil Ghosh '24
 */

public class PlayerHealth : MonoBehaviour
{
    static PlayerHealth instance;

    public int maxHealth;

    public UnityEvent OnHealthChange;
    public UnityEvent OnDeath;
    public UnityEvent OnSuccess;

    int currentHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static PlayerHealth Get() { return instance; }

    void Start()
    {
        if (TDManager.Get() != null)
        {
            maxHealth = TDManager.Get().currentSetting.startingHealth;
        }

        currentHealth = maxHealth;
    }

    public void SubtractHealth(int healthChange)
    {
        currentHealth -= healthChange;
        if(currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
        OnHealthChange.Invoke();
    }

    public int GetHealth() { return currentHealth; }
    public bool IsPlayerDead() { return currentHealth <= 0; }
    public void SetHealth(int newHealth) { currentHealth = newHealth; }
}
