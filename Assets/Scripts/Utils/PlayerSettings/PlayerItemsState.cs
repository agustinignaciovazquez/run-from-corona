using System.Collections;
using System.Collections.Generic;
using UnityEngine;
        
public class PlayerItemsState : MonoBehaviour {

    public static PlayerItemsState Instance { get; private set; }

    public ShopItem currentWeapon;
    public ShopItem currentSkin;
    public ShopItem currentJetpack;

    private void Awake(){

        if (Instance == null){

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    //Rest of your class code

}