using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private IEnumerable<ShopItem> shopItem;
    private ShopItemsList shopItemsList;
    
    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private GameObject couldPurchaseAlert;
    [SerializeField] private GameObject couldNotPurchaseAlert;

    [SerializeField] private int shopType;
    
    private void Start()
    {
        shopItemsList = ShopItemsList.Instance;
        PopulateShop();
    }

    private void PopulateShopItems()
    {
        if (shopType == 0)
            shopItem = shopItemsList.Weapons;
        else if (shopType == 1)
            shopItem = shopItemsList.Skins;
        else
            shopItem = shopItemsList.Jetpacks;
        
    }
    
    private void PopulateShop()
    {
        PopulateShopItems();
        for (int i = 0; i < shopItem.Count(); i++)
        {
            ShopItem si = shopItem.ElementAt(i);
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);
           
            itemObject.transform.GetChild(0).GetComponent<Text>().text = si.itemName;
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = si.sprite;
            
            if (!isItemSold(si))
            {
                itemObject.transform.GetChild(2).GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = si.cost.ToString();
                itemObject.transform.GetChild(2).GetComponent<Button>().transform.GetChild(1).GetComponent<Image>().sprite = si.currencySprite;
                itemObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnButtonClickBuy(si, itemObject));
            }
            else
            {
                
                itemObject.transform.GetChild(2).gameObject.SetActive(false);
                itemObject.transform.GetChild(3).gameObject.SetActive(true);
                itemObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnButtonClickEquip(si, itemObject));
            }

            if (si.itemName != PlayerPrefs.GetString("Weapon") &&
                si.itemName != PlayerPrefs.GetString("Skin") &&
                si.itemName != PlayerPrefs.GetString("Jetpack")) continue;
            itemObject.transform.GetChild(3).gameObject.SetActive(false);
            itemObject.transform.GetChild(4).gameObject.SetActive(true);

        }
    }
    

    private void OnButtonClickBuy(ShopItem item, GameObject itemObject)
    {
        bool couldPurchase = BuyItem(item);
        if (couldPurchase)
        {
            StartCoroutine(displayCouldPurchaseAlert());
            itemObject.transform.GetChild(2).gameObject.SetActive(false);
            itemObject.transform.GetChild(3).gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Cashing");
        }
        else
        {
            StartCoroutine(displayCouldNotPurchaseAlert());
            FindObjectOfType<AudioManager>().Play("FailBuy");
        }
    }
    
    private void OnButtonClickEquip(ShopItem item, GameObject itemObject)
    {
        GameObject[] gOs = GameObject.FindGameObjectsWithTag("ShopItem");
        for (int i = 0; i < gOs.Length; i++)
        {
            Destroy(gOs[i]);
        }
        
        if (shopType == 0) {
            PlayerPrefs.SetString("Weapon", item.itemName);
            PlayerItemsState.Instance.CurrentWeapon = (WeaponShopItem) item;
           
        }else if (shopType == 1){
            PlayerPrefs.SetString("Skin", item.itemName);
            PlayerItemsState.Instance.CurrentSkin = (SkinShopItem) item;
            
        }else if (shopType == 2){
            PlayerPrefs.SetString("Jetpack", item.itemName);
            PlayerItemsState.Instance.CurrentJetpack = (JetpackShopItem) item;
        }
        
        FindObjectOfType<AudioManager>().Play(item.itemName);
        PopulateShop();

        
    }
    

    private bool isItemSold(ShopItem item)
    {
        return (PlayerPrefs.GetInt(item.itemName)!=0);
    }

    private bool BuyItem(ShopItem item)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        int gems = PlayerPrefs.GetInt("Gems");
        bool isSold = isItemSold(item);

        if (!isSold)
        {
            if (item.currency == ShopItem.Currency.Coins && coins >= item.cost)
            {
                int newCoins = coins - item.cost;
                PlayerPrefs.SetInt("Coins", newCoins);
                PlayerPrefs.SetInt(item.itemName, 1);
                return true;
            }
            if (item.currency == ShopItem.Currency.Gems && gems >= item.cost)
            {
                int newGems = gems - item.cost;
                PlayerPrefs.SetInt("Gems", newGems);
                PlayerPrefs.SetInt(item.itemName, 1);
                return true;
            }
            
        }
        return false;
    }
    
    IEnumerator displayCouldPurchaseAlert()
    {
        couldPurchaseAlert.SetActive(true);
        
        yield return new WaitForSeconds(0.4f);


        couldPurchaseAlert.SetActive(false);
    }

    IEnumerator displayCouldNotPurchaseAlert()
    {
        couldNotPurchaseAlert.SetActive(true);
        
        yield return new WaitForSeconds(0.4f);

        couldNotPurchaseAlert.SetActive(false);
    }

    
}
