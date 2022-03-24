using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A HUD element displaying the total amount of health the player has
 * Written by Nikhil Ghosh '24
 */

public class HealthDisplay : MonoBehaviour
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
        text.text = PlayerHealth.Get().GetHealth().ToString();
    }
}
