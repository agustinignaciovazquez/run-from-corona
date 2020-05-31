using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerBullet : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Outbounds
            //Delete other object
            print("OUTBOUNDS");
            other.gameObject.SetActive(false);
        }
        
    }
    
}
