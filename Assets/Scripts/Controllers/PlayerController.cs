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
    public int BackgroundIndex
    {
        get => backgroundIndex;
        set => backgroundIndex = value;
    }

    public int DirectionTrigger => directionTrigger;

    public bool FlyTrigger => flyTrigger;

    public static int StateAnimId1 => StateAnimId;

    public LayerMask Ground => ground;

    public LayerMask Roof => roof;
}