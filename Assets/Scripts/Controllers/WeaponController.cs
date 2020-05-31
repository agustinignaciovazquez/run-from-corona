using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    
    private ObjectPoolSpawner objectPoolSpawner;
    private bool fireTrigger = false;
   
    [SerializeField] private float fireRate = 0.3F;
    private float nextFire = 0.0F;

    [SerializeField] private static int maxBullets = 50;
    [SerializeField] private static int currentBullets = 25;
    private static BulletTextSingleton _bulletsText;
    
    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        _bulletsText = BulletTextSingleton.SharedInstance;
        //currentBullets = maxBullets;
        _bulletsText.SetBullets(currentBullets);
    }

    // Update is called once per frame
    void FixedUpdate(){
        fireTrigger = Input.GetButton("Fire1");
        Fire();
    }


    void Fire()
    {
        if(CanShoot()) {
            var transform1 = this.transform;
            nextFire = Time.time + fireRate;
            objectPoolSpawner.SpawnObject("Bullet", transform1.position);
            ReduceAmmo();
        }
    }

    private bool CanShoot()
    {
        return (fireTrigger && Time.time > nextFire && currentBullets > 0);
    }
    
    public void AddAmmo(int amount)
    {
        print(currentBullets);
        if (currentBullets < maxBullets)
        {
            currentBullets += amount;
            if (currentBullets > maxBullets)
            {
                currentBullets = maxBullets;
            }
            _bulletsText.SetBullets(currentBullets);
        }
        print(currentBullets);
       
    }

    void ReduceAmmo()
    {
        currentBullets--;
        _bulletsText.SetBullets(currentBullets);
    }
    
    

 
}
