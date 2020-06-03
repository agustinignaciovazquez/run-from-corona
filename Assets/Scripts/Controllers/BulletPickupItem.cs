using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickupItem : AbstractPickupItem
{
    [SerializeField] private int pickupQuantity = 5;

    public override void OnPickup()
    {
        PlayerController.GetComponentInChildren<WeaponController>().AddAmmo(pickupQuantity);
    }

    public override void OnPickupAnimation()
    {
        
    }
}
