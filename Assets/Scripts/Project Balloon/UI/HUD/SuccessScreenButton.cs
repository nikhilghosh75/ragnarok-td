using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WSoft.Core;

public class SuccessScreenButton : MonoBehaviour
{
    Text text;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // I'm sorry
        text.text = TDManager.Get().inCampaign ? "Continue" : "Back to Main Menu";
    }

    void OnClick()
    {
        if(TDManager.Get().inCampaign)
        {
            TDManager.Get().ToNextCampaignScene();
        }
        else
        {
            GameManager.LoadScene("Main Menu");
        }
    }
}
