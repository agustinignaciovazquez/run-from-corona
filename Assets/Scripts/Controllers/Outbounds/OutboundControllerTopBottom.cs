using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerTopBottom : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Outbounds
        //Delete other object unless is player
        if (!(other.CompareTag("Player") || other.CompareTag("Background") || other.CompareTag("InfectedMan")
              || other.CompareTag("Green Explosion") || other.CompareTag("BloodExplosion")))
            other.gameObject.SetActive(false);
    }
    
}
