using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A singeton meant to stick around in the game
 * Written by Nikhil Ghosh '24, Andrew Zhou '22
 */

public class TDManager : MonoBehaviour
{
    static TDManager instance;

    [SerializeField]
    public DifficultySetting currentSetting;

    [HideInInspector]
    public string levelName; // Only for free play

    [HideInInspector]
    public List<string> narrativeLevels; // Only for Narrative levels

    [SerializeField]
    public List<string> campaignLevels;

    public bool inCampaign = false;
    public bool inNarrative = false;
    int currentCampaignIndex = 0;

    // Awake is called before Start
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static TDManager Get() { return instance; }

    public void StartGame()
    {
        if (inCampaign)
            StartCampaign();
        else
            WSoft.Core.GameManager.LoadScene(levelName);
    }

    public void StartLoadSave()
    {
        if (inCampaign)
        {
            StartCampaign();
        }
        else
        {
            WSoft.Core.GameManager.LoadScene(levelName, SaveSystem.Get().Load);
        }
    }

    public void StartCampaign()
    {
        WSoft.Core.GameManager.LoadScene(campaignLevels[0]);
        currentCampaignIndex = 0;
    }

    public void ToNextCampaignScene()
    {
        currentCampaignIndex++;
        if(inNarrative)
        {
            if(narrativeLevels[currentCampaignIndex] == "Main Menu")
            {
                inNarrative = false;
            }
            WSoft.Core.GameManager.LoadScene(narrativeLevels[currentCampaignIndex]);
        }
        else
        {
            if(campaignLevels[currentCampaignIndex] == "Main Menu")
            {
                inCampaign = false;
            }
            WSoft.Core.GameManager.LoadScene(campaignLevels[currentCampaignIndex]);
        }
        
    }

    public void StartNarrative(string narrativeType)
    {
        narrativeLevels.Clear();
        
        if(narrativeType == "night")
        {
            narrativeLevels.Add("Opening Cutscene 2");
        }
        else if (narrativeType == "dawn") 
        {
            narrativeLevels.Add("Ending Cutscene");
        }
        narrativeLevels.Add("Main Menu");

        WSoft.Core.GameManager.LoadScene(narrativeLevels[0]);
        currentCampaignIndex = 0;
        inNarrative = true;
    }
}
