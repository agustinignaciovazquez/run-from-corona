using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private BulletTextSingleton _bulletsText;
    //private ObjectPoolSpawner objectPoolSpawner;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask roof;
    
    //Weapon variables
    [SerializeField] private float fireRate = 0.3F;
    [SerializeField] private int maxBullets = 50;
    [SerializeField] private int currentBullets = 25;
  
    
    //Game variables
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float scrollingSpeed = 4f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 40f;
    [SerializeField] private float normalizeRotationSpeed = 3f;
    [SerializeField] private float infectionDefense = 0.01f;
    
    //Coin variables
    [SerializeField] private int initialCoins = 0;
    
    //Jetpack Energy Vars
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float energyRegen = 0.3f;
    [SerializeField] private float energySpend = 0.3f;
    [SerializeField] private EnergyBar energyBar;
    
    //FSM
    private enum State
    {
        Running,
        Flying,
        Falling,
    };
    private State state = State.Running;

    private int directionTrigger = 0;
    private bool flyTrigger = false;

    private static readonly int StateAnimId = Animator.StringToHash("State");

    
    
    //private static readonly int Walking = Animator.StringToHash("Walking");

    // Start is called before the first frame update
    void Start()
    {
        if(infectionDefense < 0 || infectionDefense > 1)
            throw new ArgumentException();
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;;
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        _bulletsText = BulletTextSingleton.SharedInstance;
        _bulletsText.SetBullets(currentBullets);
    }  
    

    // Update is called once per frame
    void Update()
    {
        directionTrigger = (int)Input.GetAxis("Horizontal");
        flyTrigger = Input.GetButton("Jump");
        SetPlayerState();
    }

    void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetPlayerMovement()
    {
        if (flyTrigger && currentEnergy >= 0.3f)
        {
            ReduceEnergy(energySpend);
            //If player not touching ground we can rotate according to movement
            if (!coll.IsTouchingLayers(ground))
            {
                RotateFlyingPlayer();
            }
            //Jetpack Force according to rotation and others
            rb.AddForce( transform.rotation * Vector2.up * jetpackForce);
        }
        else
        {
            RegenerateEnergy(energyRegen); 
        }
        
        //Normalize the rotation
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.identity, Time.deltaTime * normalizeRotationSpeed);
    }
    private void RotateFlyingPlayer()
    {
        //Player not touching the ground, we can rotate 
            Vector3 playerRotation = new Vector3(0,0,-directionTrigger * rotationSpeed);
            transform.Rotate(playerRotation);
            //Player not touching ground, we can move to the sides
            //Set player Velocity
            rb.velocity = new Vector2( directionTrigger * moveSpeed, rb.velocity.y);
        
    }
    
    private void SetPlayerState()
    {
        if (coll.IsTouchingLayers(ground))
        {
            state = State.Running;
        }else if (Math.Abs(rb.velocity.y) > 1f)
        {
            state = (flyTrigger) ? State.Flying : State.Falling;
        }
        
        anim.SetInteger(StateAnimId, (int)state);
    }

    public float GetScrollingSpeed()
    {
        return scrollingSpeed;
    }
    public float GetInfectionDefense()
    {
        return infectionDefense;
    }

    void RegenerateEnergy(float energyToRegen)
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += energyToRegen;
            energyBar.SetEnergy(currentEnergy);
        }
        
    }

    void ReduceEnergy(float energyToLose)
    {
        currentEnergy -= energyToLose;
        energyBar.SetEnergy(currentEnergy);
    }
    public void AddAmmo(int amount)
    {
        _bulletsText = BulletTextSingleton.SharedInstance;
        if (currentBullets < maxBullets)
        {
            currentBullets += amount;
            if (currentBullets > maxBullets)
            {
                currentBullets = maxBullets;
            }
        }
        _bulletsText.SetBullets(currentBullets);
    }

    public void ReduceAmmo(int amount)
    {
        currentBullets = currentBullets - amount;
        _bulletsText.SetBullets(currentBullets);
    }
    public LayerMask Ground => ground;

    public LayerMask Roof => roof;

    public float FireRate => fireRate;

    public int MaxBullets => maxBullets;

    public int CurrentBullets => currentBullets;
}