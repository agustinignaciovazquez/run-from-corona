using System.Collections;
using System.Collections.Generic;
using UnityEngine;
        
public class PlayerItemsState : MonoBehaviour {

    public static PlayerItemsState Instance { get; private set; }

    public ShopItem currentWeapon;
    public ShopItem currentSkin;
    public ShopItem currentJetpack;

    [SerializeField] private int currentBullets = 25;
    [SerializeField] private int currentCoins = 0;
    [SerializeField] private int maxBullets = 50;
    
    private void Awake(){

        if (Instance == null){

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    //Rest of your class code
    public int CurrentBullets
    {
        get => currentBullets;
        set => currentBullets = value;
    }

    public int MaxBullets
    {
        get => maxBullets;
        set => maxBullets = value;
    }

    public int CurrentCoins
    {
        get => currentCoins;
        set => currentCoins = value;
    }
}