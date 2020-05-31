using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDefaults : MonoBehaviour
{
    [SerializeField] private GameObject currentCoins;
    [SerializeField] private GameObject currentGems;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Gems", 0);
        UpdateCurrency();
        /*PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Gems", 0);
        PlayerPrefs.SetString("Weapon", "Pistol");
        PlayerPrefs.SetString("Skin", "Toby");
        PlayerPrefs.SetString("Jetpack", "Apple");
        PlayerPrefs.SetInt("Pistol", 1);
        PlayerPrefs.SetInt("Toby", 1);
        PlayerPrefs.SetInt("Apple", 1);*/
        
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
