using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupItem : AbstractPickupItem
{
    [SerializeField] private int pickupQuantity = 5;

    public override void OnPickup()
    {
        PlayerController.AddAmmo(pickupQuantity);
    }

    public override void OnPickupAnimation()
    {
        
    }
}
