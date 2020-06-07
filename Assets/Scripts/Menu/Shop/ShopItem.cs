
using UnityEngine;

public class ShopItem : ScriptableObject
{
   public enum Currency {Coins, Gems};
   public string itemName;
   public string spriteName;
   public Sprite sprite;
   public int cost;
   public Currency currency;
   public Sprite currencySprite;
}
