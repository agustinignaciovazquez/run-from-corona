using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Weapon Item")]
public class WeaponShopItem : ShopItem
{
    [SerializeField] private int damage = 10;
    [SerializeField] private string bulletSpriteTag;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int startBullets = 25;
    [SerializeField] private int maxBullets = 50;
    [SerializeField] private float fireRate = 0.3F;
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

    public float FireRate => fireRate;

    public float BulletSpeed => bulletSpeed;
}
