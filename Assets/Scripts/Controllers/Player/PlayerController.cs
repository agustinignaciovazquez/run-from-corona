﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private SpriteRenderer spriteRenderer;
    private PlayerItemsState playerItemsState;
    private ObjectPoolSpawner objectPoolSpawner;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask roof;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private JetpackController jetpackController;
    [SerializeField] private GameObject smokeEffect;
    [SerializeField] private GameObject lightningEffect;
    [SerializeField] private GameObject endGameMenu;

    [SerializeField] float horizontalSpeed = 0.003f;

    [SerializeField] private float jetpackLimit = 0.3f;
    //UI Singletons
    private CoinsTextSingleton coinsText;
    private BackgroundSettings backgroundSettings;
    
    //Player variables
    [SerializeField] private float scrollingSpeed = 4f;
    [SerializeField] private float scrollingBackgroundSpeed = 4f;
    
    private bool playerIsDead;
    
    private AudioManager audioManager;

    private bool inmunity;
    //FSM
    private enum State
    {
        Running,
        Flying,
        Falling,
    };
    private State state = State.Running;

    private float directionTrigger = 0;
    private int backgroundIndex;
    private bool flyTrigger = false;
    private float distanceTraveled = 0;
    private float jetpackJoystickScreen;
    
    private static readonly int StateAnimId = Animator.StringToHash("State");
    
    //private static readonly int Walking = Animator.StringToHash("Walking");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;

        playerItemsState = PlayerItemsState.Instance;
        playerItemsState.ReloadSettings();
        
        backgroundSettings = BackgroundSettings.Instance;
        
        audioManager = FindObjectOfType<AudioManager>();

        coinsText = CoinsTextSingleton.SharedInstance;
        coinsText.SetCoins(playerItemsState.CurrentCoins);
        jetpackJoystickScreen = Screen.width / 2f;
    }  
    

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                // Joystick Controls
                
                if(FlyTriggerCheck(touch))
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                                flyTrigger = JetpackEnergyCheck();
                            break;
                        
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                                flyTrigger = JetpackEnergyCheck();
                                directionTrigger = 0f;
                           /* Juego mas simple sin movimiento a los costados
                            // Get movement of the finger since last frame
                            float touchDeltaPositionx  = touch.position.x - (jetpackJoystickScreen + jetpackJoystickScreen/2f);

                            directionTrigger = touchDeltaPositionx * horizontalSpeed;*/
                            break;
                       
                        case TouchPhase.Ended:
                            flyTrigger = false;
                            break;
                    }
                } else if (FireCheck(touch)){
                    weaponController.Fire();
                }
            }
        }
        /* INPUT FOR UNITY ONLY DELETE IN RELEASE 
        if(Input.GetButton("Jump"))
            flyTrigger = JetpackEnergyCheck();
        else
            flyTrigger = false;
        
        if(Input.GetButton("Fire1"))
            weaponController.Fire();
        
        //directionTrigger = (int)Input.GetAxis("Horizontal");
        //flyTrigger = Input.GetButton("Jump") && jetpackController.CurrentEnergy >= 0.3f;*/
        ///* INPUT FOR UNITY ONLY DELETE IN RELEASE */

        SetPlayerState();
    }

    private bool JetpackEnergyCheck()
    {
        return jetpackController.CurrentEnergy >= jetpackLimit;
    }
    private bool FlyTriggerCheck(Touch touch)
    {
        return touch.position.x > jetpackJoystickScreen ;
    }
    
    private bool FireCheck(Touch touch)
    {
        return touch.position.x < jetpackJoystickScreen;
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
        if(distanceTraveled < 5000f)
            return scrollingBackgroundSpeed * distanceTraveled / 1000f;
        return scrollingBackgroundSpeed * 5f;
    }
    
    public float GetScrollingSpeed()
    {
        if (playerIsDead)
            return 0;
        
        if(distanceTraveled <= 25f) 
            return scrollingSpeed;
           
        if(distanceTraveled < 5000f) 
            return scrollingSpeed * (((float) Math.Log(distanceTraveled / 25f) + 2) /2);
           
        return scrollingSpeed * (((float) Math.Log(5000f / 25f) + 2) / 2);
    }

    public float DistanceTraveled
    {
        get => distanceTraveled;
        set => distanceTraveled = value;
    }

    public float GetInfectionDefense()
    {
        float infectionDefense = playerItemsState.CurrentSkin.InfectionDefense;
        
        if(infectionDefense < 0 || infectionDefense > 1)
            throw new ArgumentException();
        return infectionDefense;
    }
    
    public void AddCoin(int amount)
    {
        coinsText = CoinsTextSingleton.SharedInstance;
        playerItemsState = PlayerItemsState.Instance;
        playerItemsState.CurrentCoins += amount;
        coinsText.SetCoins(playerItemsState.CurrentCoins);
        Amplitude.Instance.logEvent("COIN_PICKUP");
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

    public BackgroundSettings.ScenarioBackground ScenarioBackgroundIndex()
    {
        return backgroundSettings.Backgrounds[backgroundIndex];
    }
    
    public void Die()
    {
        PlayerItemsState.SaveCoins();
        
        Dictionary<string, object> deathProps = new Dictionary<string, object>() {
            {"distance" ,  distanceTraveled},
            {"coins", playerItemsState.CurrentCoins}
        };
        
        Amplitude.Instance.logEvent("PLAYER_DIE", deathProps);
        endGameMenu.GetComponentInParent<EndGameMenu>().ShowOcasionalAd();
        StartCoroutine(WaiterDie());
    }
    
    IEnumerator WaiterDie()
    {
        Time.timeScale = 1f;
        Vector2 position = transform.position;
        smokeEffect.transform.position = position;
        smokeEffect.SetActive(true);
        audioManager.Play(playerItemsState.CurrentSkin.AudioName);
        playerIsDead = true;
        SetVisibility(false);
        
        yield return new WaitForSeconds(1f);
        smokeEffect.SetActive(false);
        endGameMenu.SetActive(true);
    }
    
    public void Resurrect()
    {
        Amplitude.Instance.logEvent("PLAYER_RESURRECT");
        StartCoroutine(WaiterResurrect());
    }

    IEnumerator WaiterResurrect()
    {
        objectPoolSpawner.ResetPool();
        endGameMenu.SetActive(false);
        Vector2 position = transform.position;
        lightningEffect.transform.position = new Vector3(position.x, position.y - 1);
        lightningEffect.SetActive(true);
        playerIsDead = false;
        inmunity = false;
        SetVisibility(true);
        
        yield return new WaitForSeconds(1f);
        lightningEffect.SetActive(false);
    }

    public bool Inmunity
    {
        get => inmunity;
        set => inmunity = value;
    }
    
    public void SetVisibility(bool visible)
    {
        //coll.isTrigger = visible;
        spriteRenderer.enabled = visible;
        weaponController.gameObject.SetActive(visible);
        jetpackController.gameObject.SetActive(visible);
    }
    
    public float DirectionTrigger => directionTrigger;

    public bool FlyTrigger => flyTrigger;

    public PlayerItemsState PlayerItemsState => playerItemsState;

    public LayerMask Ground => ground;

    public LayerMask Roof => roof;
    
    
}