using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A scriptable object defining how enemies spawn throughout a level
 * Written by Nikhil Ghosh '24
 */

public enum RoundType
{
    Consequtive, // Used if you want waves to spawn after each other
    Simultaneous // Used if you want waves to spawn at the exact same time
}

[CreateAssetMenu(fileName = "New Level Config", menuName ="Project Balloon/Level Config")]
public class LevelConfig : ScriptableObject
{
    [System.Serializable]
    public class Wave
    {
        [Tooltip("The enemy to spawn")]
        public GameObject enemyPrefab;

        [Tooltip("The amount of enemies to spawn")]
        public int amount;

        [Tooltip("The time between each enemy spawn")]
        public float timeBetweenEnemies; // The time bet

        [Tooltip("The path index for this enemy in this wave")]
        public int pathIndex;

        [Tooltip("Starting time in a round in simultaneous mode")]
        public float simultaneousStartingTime;
    }

    [System.Serializable]
    public class Round
    {
        [Tooltip("The time between when the \"Start Round\" button is clicked and the first enemy spawn")]
        public float startingTime;

        [Tooltip("The waves in the round")]
        public List<Wave> waves;

        [Tooltip("The type of round")]
        public RoundType roundType;

        [Tooltip("The time between each wave, if using Consequtive as the round type")]
        public float timeBetweenWaves;

        [Tooltip("The currency to give once the round ends")]
        public int currencyOnEnd;
    }

    public List<Round> rounds;

    public int GetNumRounds() { return rounds.Count; }
}
