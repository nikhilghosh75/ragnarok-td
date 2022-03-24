using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A UI script that displays the currency
 * Written by Nikhil Ghosh '24
 */

public class CurrencyDisplay : MonoBehaviour
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
        int currency = PlayerResources.Get().GetCurrency();
        text.text = WSoft.UI.FormattingFunctions.NumberWithCommas(currency);
    }
}
