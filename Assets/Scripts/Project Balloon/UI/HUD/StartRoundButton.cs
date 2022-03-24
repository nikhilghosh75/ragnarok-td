using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A button used to start the round
 * Written by Nikhil Ghosh '24
 */

public class StartRoundButton : MonoBehaviour
{

    Button button;

    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();

        EnemySpawner.Get().events.OnRoundStart.AddListener(OnRoundStart);
        EnemySpawner.Get().events.OnRoundEnd.AddListener(OnRoundEnd);
    }

    void OnRoundStart()
    {
        button.interactable = false;
    }

    void OnRoundEnd()
    {
        button.interactable = true;
    }
}
