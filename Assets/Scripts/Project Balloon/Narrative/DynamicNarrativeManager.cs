using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DynamicNarrativeCondition
{
    HealthChange,
    OnRound,
    None
}

[System.Serializable]
public class DynamicNarrativeData
{
    public DynamicNarrativeCondition condition;

    // If condition is HealthChange, positive integers will make it the threshold to reach,
    // negative will mean how much damage you need to take
    // If condition is OnRound, param is the round number to trigger.
    [Tooltip("I was too lazy to make an actual editor, sorry Crystal")]
    public int param;

    public NarrativeData narrativeData;

    [HideInInspector]
    public bool hasTriggered = false;

    public DynamicNarrativeData(NarrativeData newNarrativeData)
    {
        narrativeData = newNarrativeData;
        condition = DynamicNarrativeCondition.None;
        param = 0;
    }
}

public class DynamicNarrativeManager : MonoBehaviour
{
    public List<DynamicNarrativeData> narrativeDatas;

    public NarrativeDisplay narrativeDisplay;

    public bool displayNarrativeOnStart;
    public NarrativeData startingNarrativeData;

    private bool narrativeActive = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < narrativeDatas.Count; i++)
        {
            narrativeDatas[i].hasTriggered = false;
        }

        PlayerHealth.Get().OnHealthChange.AddListener(OnHealthChange);
        EnemySpawner.Get().events.OnRoundEnd.AddListener(OnRoundChange);

        narrativeDisplay.gameObject.SetActive(false);
        narrativeDisplay.OnDialogueEnd.AddListener(EndDialogue);

        if(displayNarrativeOnStart)
        {
            SetNarrativeData(new DynamicNarrativeData(startingNarrativeData));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHealthChange()
    {
        if (PlayerHealth.Get().IsPlayerDead()) return;

        int currentHealth = PlayerHealth.Get().GetHealth();
        int healthSubtracted = PlayerHealth.Get().maxHealth - PlayerHealth.Get().GetHealth();

        for(int i = 0; i < narrativeDatas.Count; i++)
        {
            if (narrativeDatas[i].condition != DynamicNarrativeCondition.HealthChange)
                continue;

            if (narrativeDatas[i].hasTriggered)
                continue;

            // If less than 0, look for health subtracted
            if(narrativeDatas[i].param < 0)
            {
                if(healthSubtracted >= Mathf.Abs(narrativeDatas[i].param))
                {
                    SetNarrativeData(narrativeDatas[i]);
                    break;
                }
            }
            else
            {
                if(currentHealth <= narrativeDatas[i].param)
                {
                    SetNarrativeData(narrativeDatas[i]);
                    break;
                }
            }
        }
    }

    void OnRoundChange()
    {
        if (PlayerHealth.Get().IsPlayerDead()) return;

        int currentRound = EnemySpawner.Get().GetCurrentRound();

        for (int i = 0; i < narrativeDatas.Count; i++)
        {
            if (narrativeDatas[i].condition != DynamicNarrativeCondition.OnRound)
                continue;

            if (narrativeDatas[i].hasTriggered)
                continue;

            if(currentRound + 1 == narrativeDatas[i].param)
            {
                SetNarrativeData(narrativeDatas[i]);
                break;
            }
        }
    }
    
    void SetNarrativeData(DynamicNarrativeData narrativeData)
    {
        if(narrativeActive == false)
        {
            narrativeActive = true;
            // This is incredibly bad code, but it hopefully works
            narrativeData.hasTriggered = true;

            narrativeDisplay.gameObject.SetActive(true);
            narrativeDisplay.narrativeDatas = new List<NarrativeData>();
            narrativeDisplay.narrativeDatas.Add(narrativeData.narrativeData);
            narrativeDisplay.StartNarrative();
            WSoft.Core.GameManager.PauseGame();
        }
    }

    void EndDialogue()
    {
        narrativeActive = false;
        narrativeDisplay.gameObject.SetActive(false);
        WSoft.Core.GameManager.UnpauseGame();
    }
}
