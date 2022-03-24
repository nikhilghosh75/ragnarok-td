using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A scriptable object representing a tower
 * Written by Nikhil Ghosh '24
 */

[CreateAssetMenu(fileName = "New Tower", menuName = "Project Balloon/Tower")]
[System.Serializable]
public class Tower : ScriptableObject
{

    [Tooltip("The name of the tower that appears on the menu")]
    public string towerName;

    [TextArea()]
    [Tooltip("The description that appears in the menu")]
    public string towerDescription;

    [Tooltip("The icon that appears in the menu")]
    public Sprite icon;

    [Tooltip("The cost of the tower, excluding any multipliers")]
    public int cost;

    [Tooltip("The range of the tower")]
    public float range;

    [Tooltip("The radius that the tower collider is")]
    public float radius;

    [Tooltip("The offset of the tower collider")]
    public Vector2 offset;

    [Tooltip("The damage type of the tower")]
    public WSoft.Combat.DamageType damageType;
    
    [Tooltip("Should this be treated as a trap")]
    public bool isTrap = false;

    [Tooltip("The prefab to spawn")]
    public GameObject prefab;

    [SerializeReference]
    public List<TowerUpgrade> towerUpgrades;
    // Tower portrait list
    public List <Sprite> towerPortraits;

    // Tower skin list
    [SerializeReference]
    public List<Sprite> towerSkins;
}
