using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    
    private ObjectPoolSpawner objectPoolSpawner;
    private bool fireTrigger = false;
   
    [SerializeField] private float fireRate = 0.3F;
    private float nextFire = 0.0F;

    [SerializeField] private int maxBullets = 50;
    [SerializeField] private int currentBullets = 25;
    [SerializeField] private Bullets bulletsText;
    
    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        //currentBullets = maxBullets;
        bulletsText.SetBullets(currentBullets);
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
        if (currentBullets < maxBullets)
        {
            currentBullets += amount;
            if (currentBullets > maxBullets)
            {
                currentBullets = maxBullets;
            }
            bulletsText.SetBullets(amount);
        }
       
    }

    void ReduceAmmo()
    {
        currentBullets--;
        bulletsText.SetBullets(currentBullets);
    }
    
    

 
}
