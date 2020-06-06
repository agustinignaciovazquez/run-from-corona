using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Outbounds
        //Delete other object unless is player
       // Transform parentNext = other.transform.parent;
        Transform parent = other.transform.parent;
        //TODO HACER PARA EL PADRE DE LOS PADRES
        //while (parentNext != null)
       // {
       //     parentNext = parent.parent;
       // }

        if(!(other.CompareTag("Player") || other.CompareTag("Background")))
            if(parent == null)
                other.gameObject.SetActive(false);
            else
                parent.gameObject.SetActive(false);
            

    }
    
}

