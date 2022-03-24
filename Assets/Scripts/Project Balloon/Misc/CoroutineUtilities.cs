using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/*
 * A set of general coroutine utilities that can be used anywhere
 * Written by Nikhil Ghosh
 */

public class CoroutineUtilities : MonoBehaviour
{
    // Waits for the mouse to be clicked at all
    public static IEnumerator WaitForClick(int mouseButton = 0)
    {
        while (true)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                yield break;
            }
            yield return null;
        }
    }
}

public class WaitForButtonPress : CustomYieldInstruction
{
    bool isClicked = false;
    Button button;

    public WaitForButtonPress(Button button)
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public override bool keepWaiting
    {
        get
        {
            return !isClicked;
        }
    }

    void OnButtonClick()
    {
        isClicked = true;
        // button.onClick.RemoveListener(OnButtonClick);
    }
}

