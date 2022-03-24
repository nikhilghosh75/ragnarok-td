using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class that controls the HUD. 
 * Designed so that UI Designers can freely enable/disable GameObjects and the scene will work fine.
 * Written by Nikhil Ghosh '24
 */

public class UIManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject towerMenu;
    public GameObject hideButton;
    public GameObject showButton;
    public GameObject deathScreen;
    public GameObject successScreen;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject settingsScreen;

    private bool movingTowerMenu = false;
    private bool hidden = false;

    //For Audio
    public AK.Wwise.Event WinSFX;
    public AK.Wwise.Event DeathSFX;
    public AK.Wwise.Event StopLevelSounds;
    public GameObject WwiseObject; //StopLevelMusic event doesn't work unless posted on the WwiseAudio game object

    private int timesOnDeathInvoked = 1;

    // Start is called before the first frame update
    void Start()
    {
        HUD.SetActive(true);
        deathScreen.SetActive(false);
        successScreen.SetActive(false);
        pauseMenu.SetActive(false);
        settingsScreen.SetActive(false);
        PlayerHealth.Get().OnDeath.AddListener(OnDeath);
        PlayerHealth.Get().OnSuccess.AddListener(OnSuccess);

        // setting up escape key
        KeyInputManager.Get().OnPause = pauseButton.GetComponent<UnityEngine.UI.Button>().onClick; 
        KeyInputManager.Get().OnUnpause = resumeButton.GetComponent<UnityEngine.UI.Button>().onClick;
    }

    void Update()
    {
        if (movingTowerMenu)
            MoveTowerMenu(hidden);
    }


    void OnDeath()
    {      
        Debug.Log("OnDeath has been called " + timesOnDeathInvoked + " times."); //Adding PauseGame() seems to have solved the problem, but keeping this for now for debugging purposes

        HUD.SetActive(false);
        deathScreen.SetActive(true);
        successScreen.SetActive(false);

        //Play losing theme and stop level music
        if (timesOnDeathInvoked == 1) //only post these events the first time OnDeath is invoked
        {
            WSoft.Core.GameManager.PauseGame();
            StopLevelSounds.Post(WwiseObject);
            DeathSFX.Post(WwiseObject);           
        }

        timesOnDeathInvoked++;
    }

    void OnSuccess()
    {
        //WSoft.Core.GameManager.PauseGame();
        HUD.SetActive(false);
        deathScreen.SetActive(false);
        successScreen.SetActive(true);

        //Play winning theme and stop level music
        StopLevelSounds.Post(WwiseObject);
        WinSFX.Post(WwiseObject);
    }


    public void OnPause()
    {
        pauseMenu.SetActive(true);
    }

    public void OnResume()
    {
        pauseMenu.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsScreen.SetActive(!settingsScreen.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    // Written by Rex Ma
    public void HideOrShowTowerMenu()
    {
        hidden = !hidden;
        movingTowerMenu = true;
        if (hidden)
        {
            hideButton.SetActive(false);
            showButton.SetActive(true);
        }
        else
        {
            hideButton.SetActive(true);
            showButton.SetActive(false);
        }

    }
    // Tower Menu movement
    // Written by Rex Ma
    public void MoveTowerMenu(bool hide)
    {
        var rect = towerMenu.GetComponent<RectTransform>();

        if (hide) {
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1.25f, 1);
        }
        else {
            rect.anchorMin = new Vector2(0.75f, 0);
            rect.anchorMax = new Vector2(1, 1);
        }

        var canvasScale = GetComponent<Canvas>().scaleFactor;
        var dest = new Vector2(0, 0);
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, dest, 10);
        if (Mathf.Abs(rect.anchoredPosition.x - dest.x) < 0.1f)
            movingTowerMenu = !movingTowerMenu;
    }
}
