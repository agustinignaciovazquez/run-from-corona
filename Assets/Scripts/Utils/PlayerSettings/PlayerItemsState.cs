using System.Collections;
using System.Collections.Generic;
using UnityEngine;
        
public class PlayerItemsState : MonoBehaviour {

    public static PlayerItemsState Instance { get; private set; }

    private WeaponShopItem currentWeapon;
    private ShopItem currentSkin;
    private ShopItem currentJetpack;
    
    private int currentBullets = 25;
    private int currentCoins = 0;
    private int maxBullets = 50;
    
    private void Awake(){

        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    }

    public void ReloadDefaults()
    {
        currentCoins = 0;
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

    public WeaponShopItem CurrentWeapon
    {
        get => currentWeapon;
        set => currentWeapon = value;
    }

    public ShopItem CurrentSkin
    {
        get => currentSkin;
        set => currentSkin = value;
    }

    public ShopItem CurrentJetpack
    {
        get => currentJetpack;
        set => currentJetpack = value;
    }
}