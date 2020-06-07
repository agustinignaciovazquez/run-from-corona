using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Weapon Item")]
public class WeaponShopItem : ShopItem
{
    [SerializeField] private int damage;
    [SerializeField] private string bulletSpriteTag;
    [SerializeField] private int startBullets = 25;
    [SerializeField] private int maxBullets = 50;

    public int Damage
    {
        get => damage;
    }

    public string BulletSpriteTag
    {
        get => bulletSpriteTag;
    }

    public int StartBullets
    {
        get => startBullets;
    }

    public int MaxBullets
    {
        get => maxBullets;
    }
}
