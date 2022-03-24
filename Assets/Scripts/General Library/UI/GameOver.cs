/*
 * A potential framework for a game over system
 * @ Original author Natasha Badami. Added by Nigel Charleston '21, See list of original team programmers at https://wolverinesoft-studio.itch.io/bloom-tome-of-power
 * 
 * This code may have aspects/assumptions that were specific to its original project. 
 * I would recommend using it as a reference (when implementing a new script), and adjusting it based on your team's needs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WSoft.UI
{
    public class GameOver : MonoBehaviour
    {
        public GameObject GameOverUI;
        public GameObject PauseMenuCanvas;
        public string mainMenuSceneName;
        public static bool isGameOver = false;
        public void GameOverOn()
        {
            isGameOver = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // disable the pause ability
            PauseMenuCanvas.SetActive(false);
            GameOverUI.SetActive(true);
        }

        public void LoadMainMenu()
        {
            isGameOver = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneName);
        }

        public void RestartLevel()
        {
            isGameOver = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}