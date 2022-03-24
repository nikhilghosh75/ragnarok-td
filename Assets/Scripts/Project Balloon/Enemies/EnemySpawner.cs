using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * A class the spawns enemies
 * Written by Nikhil Ghosh '24
 */

public class EnemySpawner : MonoBehaviour
{
    // Multiple paths, chosen by a specific path index
    public List<Path> paths;

    public LevelConfig levelConfig;

    public float nextRoundDelay = 3f;

    [System.Serializable]
    public class Events
    {
        public UnityEvent OnRoundStart;
        public UnityEvent OnRoundEnd;
    }

    public SpeedButton speedButton;

    public Events events;

    static EnemySpawner instance;

    bool autoStartNextRound;

    int currentRound = -1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static EnemySpawner Get() { return instance; }

    // Start is called before the first frame update
    void Start()
    {
        events.OnRoundEnd.AddListener(OnRoundEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrentRound() { return currentRound; }
    public int GetMaxRounds() { return levelConfig.rounds.Count; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void ToggleAutoStart() { autoStartNextRound = !autoStartNextRound; }
    public void StartRound()
    {
        currentRound++;
        
        if(currentRound < levelConfig.rounds.Count)
        {
            StartCoroutine(DoRound(levelConfig.rounds[currentRound]));
            events.OnRoundStart.Invoke();
        }
    }

    public void GoToRound(int newRound)
    {
        currentRound = newRound;
    }

    void AutoStartNextRound()
    {
        speedButton.OnClick();
    }

    IEnumerator DoRound(LevelConfig.Round round)
    {
        yield return new WaitForSeconds(round.startingTime);

        switch(round.roundType)
        {
            case RoundType.Consequtive:
                StartCoroutine(DoConsequtiveRound(round));
                yield break;
            case RoundType.Simultaneous:
                StartCoroutine(DoSimultaneousRound(round));
                yield break;
        }
    }

    IEnumerator DoConsequtiveRound(LevelConfig.Round round)
    {
        var waitBetweenWaves = new WaitForSeconds(round.timeBetweenWaves);
        for(int i = 0; i < round.waves.Count; i++)
        {
            LevelConfig.Wave currentWave = round.waves[i];
            for(int j = 0; j < currentWave.amount; j++)
            {
                SpawnEnemy(currentWave.enemyPrefab, currentWave.pathIndex);
                yield return new WaitForSeconds(currentWave.timeBetweenEnemies);
            }

            yield return waitBetweenWaves;
        }

        StartCoroutine(WaitForEndOfRound());
    }

    IEnumerator DoSimultaneousRound(LevelConfig.Round round)
    {
        List<float> timeTillNextSpawn = new List<float>();
        List<int> numSpawned = new List<int>();
        float currentTime = 0;
        bool roundOngoing = true;

        for(int i = 0; i < round.waves.Count; i++)
        {
            timeTillNextSpawn.Add(-0.01f + round.waves[i].simultaneousStartingTime);
            numSpawned.Add(0);
        }

        while(roundOngoing)
        {
            for(int i = 0; i < round.waves.Count; i++)
            {
                if(currentTime > timeTillNextSpawn[i])
                {
                    SpawnEnemy(round.waves[i].enemyPrefab, round.waves[i].pathIndex);
                    numSpawned[i]++;
                    timeTillNextSpawn[i] += round.waves[i].timeBetweenEnemies;
                }
            }

            roundOngoing = IsRoundOngoing(round, numSpawned);
            currentTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(WaitForEndOfRound());
    }

    IEnumerator WaitForEndOfRound()
    {
        while(true)
        {
            int childCount = transform.childCount;
            if(childCount == 0)
            {
                break;
            }
            yield return null;
        }
        events.OnRoundEnd.Invoke();
    }

    bool IsRoundOngoing(LevelConfig.Round round, List<int> numSpawned)
    {
        for(int i = 0; i < numSpawned.Count; i++)
        {
            if (numSpawned[i] < round.waves[i].amount)
                return true;
        }
        return false;
    }

    void SpawnEnemy(GameObject enemyToSpawn, int pathIndex)
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform);

        // change index based on setting
        spawnedEnemy.transform.position = paths[pathIndex].points[0];

        SetupEnemyMovement(spawnedEnemy, pathIndex);
    }

    void SetupEnemyMovement(GameObject spawnedEnemy, int pathIndex)
    {
        EnemyMovement movement = spawnedEnemy.GetComponent<EnemyMovement>();

        if(movement == null)
        {
            Debug.LogWarning("Enemy " + spawnedEnemy.name + " does not have an EnemyMovement controller");
            return;
        }

        movement.path = paths[pathIndex];
    }

    void OnRoundEnd()
    {
        if(currentRound + 1 >= levelConfig.rounds.Count)
        {
            if (!PlayerHealth.Get().IsPlayerDead())
                PlayerHealth.Get().OnSuccess.Invoke();
        }
        else
        {
            int currencyToAdd = levelConfig.rounds[currentRound].currencyOnEnd;
            PlayerResources.Get().AddCurrency(currencyToAdd);
        }

        // if toggle on, start next round in n seconds
        if (autoStartNextRound)
            Invoke("AutoStartNextRound", nextRoundDelay);
    }
}
