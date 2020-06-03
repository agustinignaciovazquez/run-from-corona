using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour{
    //Weapon variables
    [SerializeField] private float fireRate = 0.3F;
    
    //UI Singletons
    private BulletTextSingleton bulletsText;
    private ObjectPoolSpawner objectPoolSpawner;
    private PlayerItemsState playerItemsState;
    private bool fireTrigger = false;
    private float nextFire = 0.0F;

    // Start is called before the first frame update
    void Start(){
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        bulletsText = BulletTextSingleton.SharedInstance;
        playerItemsState = PlayerItemsState.Instance;
        bulletsText.SetBullets(playerItemsState.CurrentBullets);
    }

    // Update is called once per frame
    void FixedUpdate(){
        fireTrigger = Input.GetButton("Fire1");
        if (fireTrigger)
        {
            Fire();
        }
    }


    void Fire()
    {
        int currentBullets = playerItemsState.CurrentBullets;
        
        if(CanShoot(currentBullets)) {
            var transform1 = this.transform;
            nextFire = Time.time + fireRate;
            objectPoolSpawner.SpawnObject("Bullet", transform1.position);
            ReduceAmmo(1);
        }
    }
    
    private bool CanShoot(int currentBullets)
    {
        return (Time.time > nextFire && currentBullets > 0);
    }
    public void AddAmmo(int amount)
    {
        bulletsText = BulletTextSingleton.SharedInstance;
        playerItemsState = PlayerItemsState.Instance;
        if (playerItemsState.CurrentBullets < playerItemsState.MaxBullets)
        {
            playerItemsState.CurrentBullets += amount;
            if (playerItemsState.CurrentBullets > playerItemsState.MaxBullets)
            {
                playerItemsState.CurrentBullets = playerItemsState.MaxBullets;
            }
        }
        bulletsText.SetBullets(playerItemsState.CurrentBullets);
    }
    public void ReduceAmmo(int amount)
    {
        playerItemsState.CurrentBullets = playerItemsState.CurrentBullets - amount;
        bulletsText.SetBullets(playerItemsState.CurrentBullets);
    }
    
    
}
