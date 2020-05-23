using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutboundControllerLeft : MonoBehaviour
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
        //Delete other object
        print("OUTBOUNDS");
        other.gameObject.SetActive(false);
    }
    
}

