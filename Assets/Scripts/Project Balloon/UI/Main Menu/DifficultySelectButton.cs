using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A UI script that controls the display of a difficulty setting on the main menu
 * Written by Nikhil Ghosh '24
 */

public class DifficultySelectButton : MonoBehaviour
{
    public DifficultySetting difficultySetting;

    public Text title;
    public Text healthText;
    public Text goldText;

    public Image saveFileIcon;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        title.text = difficultySetting.settingName;
        healthText.text = difficultySetting.startingHealth.ToString();
        goldText.text = difficultySetting.startingMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        string levelName = TDManager.Get().levelName;
        string difficultyName = difficultySetting.settingName;
        saveFileIcon.gameObject.SetActive(SaveSystem.Get().LoadExists(levelName, difficultyName));
    }

    void OnButtonClicked()
    {
        TDManager.Get().currentSetting = difficultySetting;
        
        // Check if load file exists ONLY FOR FREE PLAY ONLY AS OF NOW
        if (!TDManager.Get().inCampaign && SaveSystem.Get().LoadExists(TDManager.Get().levelName, difficultySetting.settingName))
        {
            this.transform.root.GetComponent<MainMenuManager>().GoToLoadSaveScreen();
        }
        else {
            TDManager.Get().StartGame();
        }
    }
}
