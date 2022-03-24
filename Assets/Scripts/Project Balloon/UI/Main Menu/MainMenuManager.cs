using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A UI Script that allows the main menu to transition correctly
 * Maybe this wasn't a good idea to write it like this
 * @Nikhil Ghosh
 * @Andrew Zhou
 */

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject modeSelectScreen;
    public GameObject levelSelectScreen;
    public GameObject difficultyScreen;
    public GameObject loadSaveScreen;
    public GameObject creditsScreen;
    public GameObject settingsScreen;
    public GameObject narrativeScreen;

    /*
     * On start of game, show main menu screen and
     * deactivate all other screens
     */
    void Start()
    {
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        mainMenu.SetActive(true);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToModeSelect()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(true);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToLevelSelect()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToDifficultyScreen()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(true);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToLoadSaveScreen()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(true);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToCreditsScreen()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(true);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(false);
    }

    public void GoToSettingsScreen()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(true);
        narrativeScreen.SetActive(false);
    }

    public void GoToNararativeScreen()
    {
        mainMenu.SetActive(false);
        modeSelectScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        difficultyScreen.SetActive(false);
        loadSaveScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        narrativeScreen.SetActive(true);
    }

    // This is poor coding practice but it gets the job done
    public void GoBackFromDifficulty()
    {
        if (TDManager.Get().inCampaign)
            GoToModeSelect();
        else
            GoToLevelSelect();
    }

    public void StartNarrative(string narrativeType)
    {
        TDManager.Get().StartNarrative(narrativeType);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
