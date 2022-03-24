using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyStage {
    [Tooltip("The name of this enemy")]
    public string name;
    [Tooltip("The sprite associated with the enemy")]
    public Sprite sprite;
    [Tooltip("A multiplier applied to the speed of the enemy")]
    public float speed;
    [Tooltip("The health at which an enemy switches to this type")]
    public float healthToChange;
    [Tooltip("A toggle for the visibility of the enemy")]
    public bool camoflauged;
    [Tooltip("The damage that the enemy does to the player health")]
    public int damageToDeal;
};

[CreateAssetMenu(fileName = "New Enemy Collection", menuName = "Project Balloon/Enemy Collection")]
public class EnemyCollection : ScriptableObject {
    
    public EnemyStage[] enemies;

    // REQUIRES: name is set
    // MODIFIES: nothing
    // EFFECTS: returns the integer index of an enemy with name 
    //          from enemies. returns -1 if enemy not found.
    public int GetIndexByName(string name) {
        int index = 0;
        foreach (EnemyStage enemy in enemies) {
            if (enemy.name == name) {
                return index;
            }
            index++;
        }
        return -1;
    }
    
}
