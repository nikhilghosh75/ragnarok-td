using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStartCampaign : MonoBehaviour
{
    public DifficultySetting difficultySetting;

    public void DoTempStartCampaign()
    {
        TDManager.Get().levelName = "monkey_meadows_clone";
        TDManager.Get().currentSetting = difficultySetting;
        WSoft.Core.GameManager.LoadScene("Opening Cutscene");
    }
}
