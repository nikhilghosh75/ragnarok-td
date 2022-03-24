using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script controlling the money that the player has
 */

public class PlayerResources : MonoBehaviour
{

    static PlayerResources instance;

    int currency = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static PlayerResources Get() { return instance; }

    // Start is called before the first frame update
    void Start()
    {
        if(TDManager.Get() != null)
        {
            currency = TDManager.Get().currentSetting.startingMoney;
        }
    }

    public int GetCurrency() { return currency; }
    public void SetCurrency(int newCurrency) { currency = newCurrency; }
    public void AddCurrency(int addCurrency) { currency += addCurrency; }
}
