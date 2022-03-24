using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDManagerEvents : MonoBehaviour
{
    public void StartGame()
    {
        TDManager.Get().StartGame();
    }

    public void SetCampaign()
    {
        TDManager.Get().inCampaign = true;
    }

    public void SetFreePlayMode()
    {
        TDManager.Get().inCampaign = false;
    }

    public void ToNextCampaignScene()
    {
        TDManager.Get().ToNextCampaignScene();
    }
}
