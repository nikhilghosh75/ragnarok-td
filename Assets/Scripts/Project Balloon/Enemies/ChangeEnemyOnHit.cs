using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyOnHit : MonoBehaviour {

    [Tooltip("Used to identify what stage an enemy is at by name")]
    public string enemyName;
    public EnemyCollection enemyCollection;
    private int currentStage = 0;

    public SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement;
    private EnemyData enemyData;
    private WSoft.Combat.Health health;

    public AK.Wwise.Event EnemyDeathSFX;

    void Start() {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyData = GetComponent<EnemyData>();
        health = GetComponent<WSoft.Combat.Health>();

        health.events.OnDamage.AddListener(OnDamage);
        health.events.OnHeal.AddListener(OnHeal);
        currentStage = enemyCollection.GetIndexByName(enemyName);
    }

    void OnDamage() {
        int currentHealth = health.Current;
        for (int i = currentStage; i >= 0; i--) {
            Debug.Log($"index: {i}, desired: {enemyCollection.enemies[i].healthToChange}");
            if (currentHealth == enemyCollection.enemies[i].healthToChange) {
                SetStage(enemyCollection.enemies[i]);
                EnemyDeathSFX.Post(gameObject);
                currentStage = i;
                return;
            }
        }
    }

    void OnHeal()
    {
        int currentHealth = health.Current;
        for (int i = currentStage; i < enemyCollection.enemies.Length; i++)
        {
            Debug.Log($"index: {i}, desired: {enemyCollection.enemies[i].healthToChange}");
            if (currentHealth == enemyCollection.enemies[i].healthToChange)
            {
                SetStage(enemyCollection.enemies[i]);
                currentStage = i;
                return;
            }
        }
    }

    void SetStage(EnemyStage stage) {
        spriteRenderer.sprite = stage.sprite;
        enemyMovement.speed = stage.speed;
        enemyName = stage.name;
        enemyData.damageToDeal = stage.damageToDeal;
        health.isHidden = stage.camoflauged;
    }
}
