using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerBullet : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.StartsWith("Bullet"))
        {
            //Outbounds
            //Delete other object
            other.gameObject.SetActive(false);
        }
        
    }
    
}
