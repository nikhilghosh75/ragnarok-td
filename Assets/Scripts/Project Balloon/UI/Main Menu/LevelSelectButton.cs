using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A UI Script that enables a Level Select button to work
 * Written by Nikhil Ghosh '24
 */

public class LevelSelectButton : MonoBehaviour
{
    public string levelName;

    public Image saveFileImage;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);

        saveFileImage.gameObject.SetActive(SaveSystem.Get().LoadExists(levelName));
    }

    public void OnButtonClicked()
    {
        TDManager.Get().levelName = levelName;
    }
}
