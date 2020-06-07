
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
   public enum CurrencyEnum {Coins, Gems};
   [SerializeField] private string itemName;
   [SerializeField] private string spriteName;
   [SerializeField] private Sprite sprite;
   [SerializeField] private int cost;
   [SerializeField] private CurrencyEnum currency;
   [SerializeField] private Sprite currencySprite;

   public string ItemName => itemName;

   public string SpriteName => spriteName;

   public Sprite Sprite => sprite;

   public int Cost => cost;

   public CurrencyEnum Currency => currency;

   public Sprite CurrencySprite => currencySprite;
}
