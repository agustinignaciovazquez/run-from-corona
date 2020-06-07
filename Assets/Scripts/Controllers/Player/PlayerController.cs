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

    [SerializeField] private GameObject smokeEffect;
    [SerializeField] private GameObject lightningEffect;
    //UI Singletons
    private CoinsTextSingleton coinsText;
    private DistanceTextSingleton distanceText;
    
    //Player variables
    [SerializeField] private float scrollingSpeed = 4f;
    [SerializeField] private float scrollingBackgroundSpeed = 4f;
    [SerializeField] private float infectionDefense = 0.01f;

    [SerializeField] private GameObject endGameMenu;
    
    private bool playerIsDead;
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
        if (playerIsDead)
        {
            return 0;
        }
        
        if(distanceTraveled < 1000f)
            return scrollingBackgroundSpeed;
        if(distanceTraveled < 8000f)
            return scrollingBackgroundSpeed * distanceTraveled / 1000f;
        return scrollingBackgroundSpeed * 8f;
    }
    
    public float GetScrollingSpeed()
    {
        if (playerIsDead)
        {
            return 0;
        }

        
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
        set => scrollingSpeed = value;
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

    public void Die()
    {
        StartCoroutine(WaiterDie());
    }
    
    IEnumerator WaiterDie()
    {
        Time.timeScale = 1f;
        Vector2 position = transform.position;
        smokeEffect.transform.position = position;
        smokeEffect.SetActive(true);
        playerIsDead = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.Find("Weapon").gameObject.SetActive(false);
        gameObject.transform.Find("Jetpack").gameObject.SetActive(false);
        
        
        //This should go on EndGameMenu maybe
        coinsText.SetCoinsEnd();
        
        distanceText = DistanceTextSingleton.SharedInstance;
        distanceText.SetDistanceEnd();
        
        yield return new WaitForSeconds(1f);
        smokeEffect.SetActive(false);
        endGameMenu.SetActive(true);        
    }

    public void Resurrect()
    { 
        StartCoroutine(WaiterResurrect());
       
    }

    IEnumerator WaiterResurrect()
    {
        endGameMenu.SetActive(false);
        Vector2 position = transform.position;
        lightningEffect.transform.position = new Vector3(position.x, position.y - 1);
        lightningEffect.SetActive(true);
        playerIsDead = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.Find("Weapon").gameObject.SetActive(true);
        gameObject.transform.Find("Jetpack").gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        lightningEffect.SetActive(false);
    }

    public int DirectionTrigger => directionTrigger;

    public bool FlyTrigger => flyTrigger;

    public PlayerItemsState PlayerItemsState => playerItemsState;

    public LayerMask Ground => ground;

    public LayerMask Roof => roof;
}