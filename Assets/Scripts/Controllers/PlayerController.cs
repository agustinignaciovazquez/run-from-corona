using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private PlayerItemsState playerItemsState;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask roof;
    [SerializeField] private EnergyBar energyBar;

    //UI Singletons
    private CoinsTextSingleton coinsText;
    
    //Game variables
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float scrollingSpeed = 4f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 40f;
    [SerializeField] private float normalizeRotationSpeed = 3f;
    [SerializeField] private float infectionDefense = 0.01f;
    
    //Jetpack Energy Vars
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float energyRegen = 0.3f;
    [SerializeField] private float energySpend = 0.3f;
    
    
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
    private float distanceTraveled = 0;
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

        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        
        playerItemsState = PlayerItemsState.Instance;
     
        coinsText = CoinsTextSingleton.SharedInstance;
        coinsText.SetCoins(playerItemsState.CurrentCoins);
    }  
    

    // Update is called once per frame
    void Update()
    {
        directionTrigger = (int)Input.GetAxis("Horizontal");
        flyTrigger = Input.GetButton("Jump") && currentEnergy >= 0.3f;
        SetPlayerState();
    }

    void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetPlayerMovement()
    {
        if (flyTrigger)
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
        //TODO FIX THIS
        if(distanceTraveled < 1000f)
            return scrollingSpeed;
        if(distanceTraveled < 8000f)
            return scrollingSpeed * distanceTraveled / 1000f;
        return (float) Math.Log(distanceTraveled) * scrollingSpeed;
    }

    public float DistanceTraveled
    {
        get => distanceTraveled;
        set => distanceTraveled = value;
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

    public void AddCoin(int amount)
    {
        coinsText = CoinsTextSingleton.SharedInstance;
        playerItemsState = PlayerItemsState.Instance;
        playerItemsState.CurrentCoins += amount;
        coinsText.SetCoins(playerItemsState.CurrentCoins);
    }

    public float ScrollingSpeed
    {
        get => scrollingSpeed;
        set => scrollingSpeed = value;
    }

    public LayerMask Ground => ground;

    public LayerMask Roof => roof;
}