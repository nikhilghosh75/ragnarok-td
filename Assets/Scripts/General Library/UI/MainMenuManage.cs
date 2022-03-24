/*
 * Utilities for managing a main menu screen
 * Written by Angela Salacata '?, Natasha Badami '20
 * NOT APPROVED
 * 
 * This code may have aspects/assumptions that were specific to its original project and hard coded values 
 * I would recommend using it as a reference (when implementing a new script), and heavily adjusting it based on your team's needs
 */
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace WSoft.UI
{
    public class MainMenuManage : MonoBehaviour
    {
        // Dev variable
        public string otherMenuName;

        public List<string> SceneSelectionNames;
        [SerializeField] GameObject buttonPrefab;

        Button[] buttons;
        [SerializeField] Transform menuPanel;
        [SerializeField] GameObject titleArt;
        [SerializeField] GameObject buttonContainer;


        public void Start()
        {
            buttons = buttonContainer.GetComponentsInChildren<Button>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            foreach (string str in SceneSelectionNames)
            {
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.GetComponentInChildren<Text>().text = str.ToString();
                button.GetComponent<Button>().onClick.AddListener(
                  () => { LoadChosenScene(str); }
                );
                button.transform.parent = menuPanel;
            }
        }

        void LoadChosenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TEST_Main_Menu")
            {
                Cursor.visible = true;
            }
        }

        public void ShowCreditsOrOptions(GameObject menuToShow)
        {
            // disable objects other than background (button container and title art) and show the menu
            buttonContainer.SetActive(false);
            titleArt.SetActive(false);
            menuToShow.SetActive(true);
        }

        public void HideCreditsOrOptions(GameObject menuToHide)
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            menuToHide.SetActive(false);

            for (int i = 0; i < 3; ++i)
            {
                Button butn = buttons[i];
                TextMeshProUGUI textOfButton = butn.GetComponentInChildren<TextMeshProUGUI>();
                textOfButton.color = new Color32(255, 255, 255, 0);

                RectTransform toResetTransform = butn.GetComponent<RectTransform>();
                float x = (toResetTransform.localScale.x - 1) * -1.0f;
                float y = (toResetTransform.localScale.y - 1) * -1.0f;
                toResetTransform.localScale += new Vector3(x, y, 0.0f);
            }

            Button quitButton = buttons[3];
            TextMeshProUGUI[] textOfQuitButton = quitButton.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(textOfQuitButton[1].text);
            textOfQuitButton[1].color = new Color32(255, 255, 255, 0);

            RectTransform quitResetTransfrom = quitButton.GetComponent<RectTransform>();
            float xQuit = (quitResetTransfrom.localScale.x - 1) * -1.0f;
            float yQuit = (quitResetTransfrom.localScale.y - 1) * -1.0f;
            quitResetTransfrom.localScale += new Vector3(xQuit, yQuit, 0.0f);

            titleArt.SetActive(true);
            buttonContainer.SetActive(true);
        }

        public void GoToCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        // Dev code
        public void SwitchToOtherMenu()
        {
            SceneManager.LoadScene(otherMenuName);
        }

    }
}