using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelConfigBuilder : EditorWindow
{
    int currentlySelectedTab;

    // General
    int numRounds;
    string configName;

    // From Multiplier
    [System.Serializable]
    public struct FromMultiplierEnemy
    {
        public GameObject enemyPrefab;
        public int startingRoundNumber;
        public int startingAmount;
        public float startingTimeBetweenEnemies;
        public float amountMultiplier;
        public float timeBetweenEnemiesMultiplier;
        public int spawnEveryXRounds;
    }

    public List<FromMultiplierEnemy> enemies = new List<FromMultiplierEnemy>();
    int currencyOnEnd;
    float startingTime;

    // From Existing
    LevelConfig config;
    float timeMultiplier = 1;
    float amountMultiplier = 1;

    [MenuItem("Project Balloon/Level Config Builder")]
    public static void OpenWindow()
    {
        GetWindow<LevelConfigBuilder>(false, "Level Config Builder", true);
    }

    private void OnGUI()
    {
        currentlySelectedTab = GUILayout.Toolbar(currentlySelectedTab,
            new string[] { "From Multipliers", "From Existing" });

        switch(currentlySelectedTab)
        {
            // From Multipliers
            case 0:
                FromMultipliers();
                break;
            case 1:
                FromExisting();
                break;
        }
    }

    void FromMultipliers()
    {
        GUILayout.Label("This allows you to easily generate a Level Config by setting how you want to enemies to grow");

        // Config Name
        configName = EditorGUILayout.TextField("Config Name", configName);

        // Num Rounds
        numRounds = EditorGUILayout.IntField("Number of Rounds", numRounds);

        // CurrencyOnEnd
        currencyOnEnd = EditorGUILayout.IntField("Currency On Round End", currencyOnEnd);

        // Starting Time
        startingTime = EditorGUILayout.FloatField("Starting Time", startingTime);

        // Enemies
        SerializedObject so = new SerializedObject(this);
        SerializedProperty enemiesProperty = so.FindProperty("enemies");
        EditorGUILayout.PropertyField(enemiesProperty, true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Build Level Config"))
        {
            BuildFromMultipliers();
        }
    }

    void BuildFromMultipliers()
    {
        LevelConfig newConfig = ScriptableObject.CreateInstance<LevelConfig>();
        newConfig.rounds = new List<LevelConfig.Round>();

        for(int i = 0; i < numRounds; i++)
        {
            LevelConfig.Round round = new LevelConfig.Round();
            round.currencyOnEnd = currencyOnEnd;
            round.waves = new List<LevelConfig.Wave>();
            round.startingTime = startingTime;

            newConfig.rounds.Add(round);
        }

        foreach (FromMultiplierEnemy enemy in enemies)
        {
            int currentAmount = enemy.startingAmount;
            float currentTimeBetweenEnemies = enemy.startingTimeBetweenEnemies;

            for (int i = enemy.startingRoundNumber; i < numRounds; i += enemy.spawnEveryXRounds)
            {
                LevelConfig.Wave wave = new LevelConfig.Wave();
                wave.enemyPrefab = enemy.enemyPrefab;
                wave.amount = currentAmount;
                wave.timeBetweenEnemies = currentTimeBetweenEnemies;

                newConfig.rounds[i].waves.Add(wave);

                currentAmount = (int)(currentAmount * enemy.amountMultiplier);
                currentTimeBetweenEnemies *= enemy.timeBetweenEnemiesMultiplier;
            }
        }

        string filepath = "Assets/" + configName + ".asset";
        AssetDatabase.CreateAsset(newConfig, filepath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = newConfig;
    }

    void FromExisting()
    {
        GUILayout.Label("This allows you to easily generate a Level Config from an existing config to make it harder/easier");
        GUILayout.Label("Set TimeMultiplier higher to make the new level easier, lower to make it harder");
        GUILayout.Label("Set AmountMultiplier higher to make the new level harder, lower to make it easier");

        // Config
        config = (LevelConfig)EditorGUILayout.ObjectField("Original Level Config", config, typeof(LevelConfig), false);
        configName = EditorGUILayout.TextField("Config Name", configName);

        // Num Rounds
        GUILayout.BeginHorizontal();
        numRounds = EditorGUILayout.IntField("Number of Rounds", numRounds);
        if(GUILayout.Button("Reset to Original"))
        {
            if (config != null)
            {
                numRounds = config.GetNumRounds();
            }
        }
        GUILayout.EndHorizontal();

        // Time Multiplier
        timeMultiplier = EditorGUILayout.FloatField(new GUIContent("Time Multiplier", 
            "What the time between waves will be multiplied by"), timeMultiplier);

        // Amount Multiplier
        amountMultiplier = EditorGUILayout.FloatField(new GUIContent("Amount Multiplier",
            "What the amount of enemies per wave will be multiplied by"), amountMultiplier);

        if(GUILayout.Button("Build Level Config"))
        {
            BuildFromExisting();
        }
    }

    LevelConfig.Round BuildRound(LevelConfig.Round baseRound, float timeMult, float amountMult)
    {
        LevelConfig.Round round = new LevelConfig.Round();
        round.currencyOnEnd = baseRound.currencyOnEnd;
        round.roundType = baseRound.roundType;
        round.startingTime = baseRound.startingTime;
        round.timeBetweenWaves = baseRound.timeBetweenWaves * timeMult;
        round.waves = new List<LevelConfig.Wave>();

        for(int i = 0; i < baseRound.waves.Count; i++)
        {
            LevelConfig.Wave wave = new LevelConfig.Wave();
            wave.enemyPrefab = baseRound.waves[i].enemyPrefab;
            wave.amount = (int)(baseRound.waves[i].amount * amountMult);
            wave.timeBetweenEnemies = baseRound.waves[i].timeBetweenEnemies * timeMult;
            round.waves.Add(wave);
        }

        return round;
    }

    void BuildFromExisting()
    {
        if (config == null)
            return;

        LevelConfig newConfig = ScriptableObject.CreateInstance<LevelConfig>();
        newConfig.rounds = new List<LevelConfig.Round>();

        for(int i = 0; i < numRounds; i++)
        {
            int roundIndex = i % config.GetNumRounds();
            newConfig.rounds.Add(BuildRound(config.rounds[roundIndex], 
                timeMultiplier, amountMultiplier));
        }

        string filepath = "Assets/" + configName + ".asset";
        AssetDatabase.CreateAsset(newConfig, filepath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = newConfig;
    }
}
