using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCurrencies : MonoBehaviour
{
    [SerializeField] private GameObject currentCoins;
    [SerializeField] private GameObject currentGems;
    
    // Start is called before the first frame update
    void Start()
    {
        
        /*DEBUG COINS
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Gems", 100);
        PlayerPrefs.SetInt("Coins", 50000);
        */
        UpdateCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCurrency()
    {
        currentCoins.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
        currentGems.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("Gems").ToString();
    }
}
