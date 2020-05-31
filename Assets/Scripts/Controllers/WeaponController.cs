using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    
    [SerializeField] private PlayerController playerController;
    private ObjectPoolSpawner objectPoolSpawner;
    private bool fireTrigger = false;
    private float fireRate;
    private float nextFire = 0.0F;

    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        fireRate = playerController.FireRate;
    }

    // Update is called once per frame
    void FixedUpdate(){
        fireTrigger = Input.GetButton("Fire1");
        Fire();
    }


    void Fire()
    {
        int currentBullets = playerController.CurrentBullets;
        
        if(CanShoot(currentBullets)) {
            var transform1 = this.transform;
            nextFire = Time.time + fireRate;
            objectPoolSpawner.SpawnObject("Bullet", transform1.position);
            playerController.ReduceAmmo(1);
        }
    }

    private bool CanShoot(int currentBullets)
    {
        return (fireTrigger && Time.time > nextFire && currentBullets > 0);
    }
    
}
