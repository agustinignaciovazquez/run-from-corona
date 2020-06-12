using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerAll : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Outbounds
        //Delete other object unless is player
       // Transform parentNext = other.transform.parent;
        Transform parent = other.transform.parent;
        Transform parentNext = parent;
        while (parent != null && parentNext != null)
        {
            parent = parentNext;
            parentNext = parent.parent;
        };
        //print(other.tag);
        if(!(other.CompareTag("Player") || other.CompareTag("Background")))
            if(parent == null)
                other.gameObject.SetActive(false);
            else
                parent.gameObject.SetActive(false);
            

    }
    
}

