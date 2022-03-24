using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that emmulates Audio Events with WWise Events. If WWise is not being used, this script won't be compiled
 * 
 * Scriptable objects specialized for audio events. allows audio team work to be
 * completely decoupled from all other work, minimizing risk of merge conflicts during the workflow
 * Written by Nico Williams '20, Faulkner Bodbyl-Mast '21, Nikhil Ghosh '24
 * NOT APPROVED
 */

#if WSOFT_WWISE

namespace WSoft.Audio.WWise
{
    [System.Serializable]
    public class AudioEvent
    {
        public AK.Wwise.Event wwiseEvent;

        public void PlaySound(GameObject caller)
        {
            // post wwise event
            if (wwiseEvent.IsValid())
            {
                Debug.Log(wwiseEvent);
                wwiseEvent.Post(caller);
            }
            // audio not yet created
            else
            {
                Debug.LogWarning("Warning: missing audio for audio event on " + caller.name);
            }
        }
    }
}

#endif