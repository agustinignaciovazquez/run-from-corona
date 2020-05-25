using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    
    private ObjectPoolSpawner objectPoolSpawner;
    private bool fireTrigger = false;
   
    [SerializeField] private float fireRate = 0.3F;
    private float nextFire = 0.0F;

    public int maxBullets = 25;
    public int currentBullets = 0;
    public Bullets bullets;
    
    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        currentBullets = maxBullets;
        bullets.SetBullets(maxBullets);
    }

    // Update is called once per frame
    void FixedUpdate(){
        fireTrigger = Input.GetButton("Fire1");
        Fire();
    }


    void Fire()
    {
        if(fireTrigger && Time.time > nextFire && currentBullets > 0)
        {
            var transform1 = this.transform;
            nextFire = Time.time + fireRate;
            objectPoolSpawner.SpawnObject("Bullet", transform1.position);
            ReduceAmmo();
        }
    }
    
    void AddAmmo(int amount)
    {
        currentBullets += amount;
        bullets.SetBullets(amount);
    }

    void ReduceAmmo()
    {
        currentBullets--;
        bullets.SetBullets(currentBullets);
    }
    
    

 
}
