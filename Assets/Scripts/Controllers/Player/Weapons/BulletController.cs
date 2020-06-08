using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, ObjectPoolInterface
{
    //private Rigidbody2D rb;
    private Rigidbody2D rb;
    private Collider2D coll;
    private float damage;
    private float bulletSpeed = 10f;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        WeaponShopItem weaponShopItem = PlayerItemsState.Instance.CurrentWeapon;
        damage = weaponShopItem.Damage;
        bulletSpeed = weaponShopItem.BulletSpeed;
    }
    
    public void OnObjectSpawn()
    {
        rb.velocity = new Vector2(   bulletSpeed, rb.velocity.y);
    }
}
