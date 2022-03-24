using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Collection", menuName = "Project Balloon/Tower Collection")]
public class TowerCollection : ScriptableObject
{
    public List<Tower> towers;
}
