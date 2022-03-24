using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tower))]
public class TowerEditor : Editor
{
    Tower tower;

    public void OnEnable()
    {
        tower = (Tower)target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("+ Damage Upgrade"))
            tower.towerUpgrades.Add(new DamageUpgrade());
        if (GUILayout.Button("+ Range Upgrade"))
            tower.towerUpgrades.Add(new RangeUpgrade(1f));
        if (GUILayout.Button("+ Num Shots Upgrade"))
            tower.towerUpgrades.Add(new NumShotsUpgrade());
        if (GUILayout.Button("+ Fire Rate Upgrade"))
            tower.towerUpgrades.Add(new FireRateUpgrade(1f));
        if (GUILayout.Button("+ See Hidden Enemies Upgrade"))
            tower.towerUpgrades.Add(new SeeHiddenEnemiesUpgrade());
        if (GUILayout.Button("+ Projectile Speed Upgrade"))
            tower.towerUpgrades.Add(new ProjectileSpeedUpgrade());
        if (GUILayout.Button("+ Projectile Health Upgrade"))
            tower.towerUpgrades.Add(new ProjectileHealthUpgrade());
        if (GUILayout.Button("+ Stun Enable Upgrade"))
            tower.towerUpgrades.Add(new EnableStunUpgrade());
        if (GUILayout.Button("+ Explosive Blast Upgrade"))
            tower.towerUpgrades.Add(new ExplosiveBlastUpgrade());
        if (GUILayout.Button("+ Support Tower Upgrade"))
            tower.towerUpgrades.Add(new SupportTowerUpgrade());

        EditorUtility.SetDirty(tower);
    }
}
