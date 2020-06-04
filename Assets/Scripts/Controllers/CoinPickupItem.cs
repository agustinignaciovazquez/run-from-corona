using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupItem : AbstractPickupItem
{
    [SerializeField] private int pickupQuantity = 1;

    public override void OnPickup()
    {
        FindObjectOfType<AudioManager>().Play("Coin");
        PlayerController.AddCoin(pickupQuantity);
    }

    public override void OnPickupAnimation()
    {
        
    }
}
