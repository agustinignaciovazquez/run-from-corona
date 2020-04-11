using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    
    private ObjectPoolSpawner objectPoolSpawner;
    private bool fireTrigger = false;
   
    [SerializeField] private float fireRate = 0.3F;
    private float nextFire = 0.0F;

    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
    }

    // Update is called once per frame
    void FixedUpdate(){
        fireTrigger = Input.GetButton("Fire1");
        Fire();
    }


    void Fire()
    {
        if(fireTrigger && Time.time > nextFire)
        {
            var transform1 = this.transform;
            nextFire = Time.time + fireRate;
            objectPoolSpawner.SpawnObject("Bullet", transform1.position, transform1.rotation);
        }
    }
    
    

 
}
