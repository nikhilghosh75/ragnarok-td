using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/*
 * A class that manages all the key inputs
 * Edit: Rex Ma
 */
public class KeyInputManager : MonoBehaviour
{
    static KeyInputManager instance;
    public UnityEvent OnPause;
    public UnityEvent OnUnpause;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static KeyInputManager Get() { return instance; }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            
            if (WSoft.Core.GameManager.gamePaused)
            {
                OnUnpause.Invoke();
            }

            else
            {
                OnPause.Invoke();
            }
        }
    }
}
