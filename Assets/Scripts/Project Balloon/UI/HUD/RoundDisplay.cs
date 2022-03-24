using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A HUD Element that displays the current round that the player is on
 * Written by Nikhil Ghosh '24
 */

public class RoundDisplay : MonoBehaviour
{

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Round " + (EnemySpawner.Get().GetCurrentRound() + 1).ToString() + "/" + EnemySpawner.Get().GetMaxRounds();
    }
}
