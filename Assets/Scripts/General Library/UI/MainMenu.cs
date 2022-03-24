/*
 * A potential framework for a main menu system
 * Written by Angela Salacata '?, Natasha Badami '20, George Castle '22
 * NOT APPROVED
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

    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("TeamA-Level");
        }

        public void QuitGame()
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }
    }
}