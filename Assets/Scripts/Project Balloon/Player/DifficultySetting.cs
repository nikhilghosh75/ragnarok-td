using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A scriptable object representing difficulty
 */

[CreateAssetMenu(fileName = "New Difficulty Setting", menuName = "Project Balloon/Difficulty Setting")]
public class DifficultySetting : ScriptableObject
{
    [Tooltip("The name that will be displayed in the main menu")]
    public string settingName;

    [Tooltip("The money that the player will start with")]
    public int startingMoney;

    [Tooltip("The health that the player will start with")]
    public int startingHealth;

    [Tooltip("The ratio between the sell price and the purchase price")]
    public float sellRatio;
}
