using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsList : MonoBehaviour
{
    public static ShopItemsList Instance { get; private set; }

    [SerializeField] private List<WeaponShopItem> weapons;
    [SerializeField] private List<SkinShopItem> skins;
    [SerializeField] private List<JetpackShopItem> jetpacks;
    
    private void Awake(){

        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    }

    public List<WeaponShopItem> Weapons => weapons;

    public List<SkinShopItem> Skins => skins;

    public List<JetpackShopItem> Jetpacks => jetpacks;
}
