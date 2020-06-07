using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
        
public class PlayerItemsState : MonoBehaviour {

    public static PlayerItemsState Instance { get; private set; }

    private List<WeaponShopItem> weaponShopItems;
    private List<JetpackShopItem> jetpackShopItems;
    private List<SkinShopItem> skinShopItems;
    
    private ShopItemsList shopItemsList;
    
    private WeaponShopItem currentWeapon;
    private SkinShopItem currentSkin;
    private JetpackShopItem currentJetpack;
    
    private int currentCoins = 0;
    private int currentBullets = 0;
    private float currentEnergy = 0;
    
    private void Start(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetDefaultsSettings();
            ReloadSettings();
        } else {
            Destroy(this);
        }
    }

    private void SetDefaultsSettings()
    {
        //Set default items
        PlayerPrefs.SetInt("Juan", 1);
        PlayerPrefs.SetInt("Water", 1);
        PlayerPrefs.SetInt("JetpackStandard", 1);
        
        if (HasPreference("Weapon"))
            PlayerPrefs.SetString("Weapon", "Water");
        if (HasPreference("Skin"))
            PlayerPrefs.SetString("Skin", "Juan");
        if (HasPreference("Jetpack"))
            PlayerPrefs.SetString("Jetpack", "JetpackStandard");
        if(!PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", 0);
        if(!PlayerPrefs.HasKey("Gems"))
            PlayerPrefs.SetInt("Gems", 0);
    }

    private bool HasPreference(string pref)
    {
        return (!PlayerPrefs.HasKey(pref) || PlayerPrefs.GetString(pref) == "");
    }
    public void ReloadSettings()
    {
        shopItemsList = ShopItemsList.Instance;
        
        weaponShopItems = shopItemsList.Weapons;
        jetpackShopItems = shopItemsList.Jetpacks;
        skinShopItems = shopItemsList.Skins;
        /*print(PlayerPrefs.GetString("Weapon"));
        print(PlayerPrefs.GetString("Jetpack"));
        print(PlayerPrefs.GetString("Skin"));*/
        currentWeapon = (WeaponShopItem) GetCurrentItem(PlayerPrefs.GetString("Weapon"), weaponShopItems);
        currentJetpack = (JetpackShopItem) GetCurrentItem(PlayerPrefs.GetString("Jetpack"), jetpackShopItems);
        currentSkin = (SkinShopItem) GetCurrentItem(PlayerPrefs.GetString("Skin"), skinShopItems);
        
        currentCoins = 0;
        currentBullets = currentWeapon.StartBullets;
        currentEnergy = currentJetpack.StartEnergy;
    }

    public ShopItem GetCurrentItem(string itemName, IEnumerable<ShopItem> shopItems)
    {
        ShopItem si = shopItems.First( s => itemName.Equals(s.ItemName)); 
        if(si == null)
            throw new UnassignedReferenceException();
        return si;
    }
    
    public int CurrentCoins
    {
        get => currentCoins;
        set => currentCoins = value;
    }
    
    public int CurrentBullets
    {
        get => currentBullets;
        set => currentBullets = value;
    }

    public float CurrentEnergy
    {
        get => currentEnergy;
        set => currentEnergy = value;
    }

    public WeaponShopItem CurrentWeapon
    {
        get => currentWeapon;
    }

    public SkinShopItem CurrentSkin
    {
        get => currentSkin;
    }

    public JetpackShopItem CurrentJetpack
    {
        get => currentJetpack;
    }
}