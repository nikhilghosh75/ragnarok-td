using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class designed to call static GameManager events from UnityEvents
 * Written by Nikhil Ghosh '24
 */

namespace WSoft.Core
{
    public class GameManagerEvents : MonoBehaviour
    {
        public void StartGame(string startSceneName)
        {
            GameManager.StartGame(startSceneName);
        }

        public void ReturnToMainMenu()
        {
            GameManager.ReturnToMainMenu();
        }

        public void LoadScene(string sceneName)
        {
            GameManager.LoadScene(sceneName);
        }

        public void LoadSceneAsync(string sceneName)
        {
            GameManager.LoadSceneAsync(sceneName);
        }

        public void GameOver()
        {
            GameManager.GameOver();
        }

        public void QuitGame()
        {
            GameManager.QuitGame();
        }

        public void RestartScene()
        {
            GameManager.RestartScene();
        }

        public void PauseGame()
        {
            GameManager.PauseGame();
        }

        public void UnpauseGame()
        {
            GameManager.UnpauseGame();
        }

        public void TogglePause()
        {
            GameManager.TogglePause();
        }
    }
}
