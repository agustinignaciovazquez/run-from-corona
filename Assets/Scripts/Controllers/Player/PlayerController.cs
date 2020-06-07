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
    [SerializeField] private JetpackController jetpackController;
    
    //UI Singletons
    private CoinsTextSingleton coinsText;
    
    //Player variables
    [SerializeField] private float scrollingSpeed = 4f;
    [SerializeField] private float scrollingBackgroundSpeed = 4f;
    [SerializeField] private float infectionDefense = 0.01f;
    
    //FSM
    private enum State
    {
        Running,
        Flying,
        Falling,
    };
    private State state = State.Running;

    private int directionTrigger = 0;
    private int backgroundIndex;
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
        
        playerItemsState = PlayerItemsState.Instance;
        playerItemsState.ReloadSettings();
        
        coinsText = CoinsTextSingleton.SharedInstance;
        coinsText.SetCoins(playerItemsState.CurrentCoins);
    }  
    

    // Update is called once per frame
    void Update()
    {
        directionTrigger = (int)Input.GetAxis("Horizontal");
        flyTrigger = Input.GetButton("Jump") && jetpackController.CurrentEnergy >= 0.3f;
        SetPlayerState();
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

    public float GetBackgroundScrollSpeed()
    {
        if(distanceTraveled < 1000f)
            return scrollingBackgroundSpeed;
        if(distanceTraveled < 8000f)
            return scrollingBackgroundSpeed * distanceTraveled / 1000f;
        return scrollingBackgroundSpeed * 8f;
    }
    
    public float GetScrollingSpeed()
    {
      
          if(distanceTraveled <= 25f)
               return scrollingSpeed;
           
           if(distanceTraveled < 8000f)
               return scrollingSpeed * ((float) Math.Log(distanceTraveled / 25f) + 1);
           
           return scrollingSpeed * ((float) Math.Log(8000f / 100f) + 1);
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
    }
    
    public float ScrollingBackgroundSpeed
    {
        get => scrollingBackgroundSpeed;
        set => scrollingBackgroundSpeed = value;
    }
    public int BackgroundIndex
    {
        get => backgroundIndex;
        set => backgroundIndex = value;
    }

    public int DirectionTrigger => directionTrigger;

    public bool FlyTrigger => flyTrigger;

    public PlayerItemsState PlayerItemsState => playerItemsState;

    public LayerMask Ground => ground;

    public LayerMask Roof => roof;
}