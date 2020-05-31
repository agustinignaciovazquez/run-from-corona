using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickupItem : AbstractPickupItem
{
    private WeaponController weaponController;
    [SerializeField] private int pickupQuantity = 5;
    protected virtual void Start()
    {
        weaponController = PlayerController.Weapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPickup()
    {
        weaponController.AddAmmo(pickupQuantity);
    }

    public override void OnPickupAnimation()
    {
        
    }
}
