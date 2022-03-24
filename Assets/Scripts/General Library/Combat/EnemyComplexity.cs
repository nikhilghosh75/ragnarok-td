/*
 * Enemy Complexity
 * Defines the 'complexity' value needed for enemy balance calculations.
 * The EnemyBalance editor window determines which game objects are enemies based on
 * which ones have this component.
 *
 * Author: Michael Tilbury (email:mtilbury@umich.edu, discord:zafuru#8634)
 * NOT APPROVED
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComplexity : MonoBehaviour
{
    [Header("Relative complexity used for EnemyBalance calculations")]
    public float ComplexityFactor = 1.0f;
}
