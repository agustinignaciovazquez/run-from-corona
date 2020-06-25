using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, ObjectPoolInterface
{
    private RandomSingleton random;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private float scrollVelocity;
    private ObjectPoolSpawner objectPoolSpawner;
    private GameObject player;
    [SerializeField] private float infectProbability = 0.99f;
    [SerializeField] private float factorProportionalSpeed = 1f;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //Check args are correct
        if(infectProbability < 0 || infectProbability > 1 || factorProportionalSpeed < 0)
            throw new ArgumentException();
        
        //Initializate variables
        random = RandomSingleton.GetSharedInstance;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Covid") || other.gameObject.CompareTag("CoinPickup") || other.gameObject.CompareTag("PickupBullet")) 
            other.gameObject.SetActive(false);
        
        //Player Collision
        if (other.gameObject.CompareTag(player.tag))
        {
            var infectionPlayerProbability = InfectPlayerProbability();
            
            if(playerController.Inmunity == false && random.RollDice(infectionPlayerProbability))
            {
                Amplitude.Instance.logEvent("ENEMY_KILL_PLAYER");
                playerController.Die();
            }
            else
            {
                Amplitude.Instance.logEvent("PLAYER_SURVIVE_VIRUS");
                //print("SAFASTE");
            }
            //Auto-Death
            OnEnemyDeathAnimation();
            gameObject.SetActive(false);
        }
        
        //Desinfect Bullet Collision
        if (other.gameObject.tag.StartsWith("Bullet"))
        {
            Amplitude.Instance.logEvent("PLAYER_KILL_VIRUS");
            //Delete bullet
            other.gameObject.SetActive(false);
            
            //Animate explosion
            OnEnemyDeathAnimation();
            
            //Auto delete ourselves
            this.gameObject.SetActive(false);
        }
    }
    
    public float GetInfectProbability()
    {
        return this.infectProbability;
    }
    
    private float InfectPlayerProbability()
    {
        float playerInfectionDefense = playerController.GetInfectionDefense() > 0 ? playerController.GetInfectionDefense() : 0;
        float infectionProbability = GetInfectProbability() - playerInfectionDefense;
        return infectionProbability > 0 ? infectionProbability : 0;
    }
    protected virtual void Update()
    {
        //Update velocity acording to player every frame
        SetBackgroundVelocity();
    }
    public void SetBackgroundVelocity()
    {
        scrollVelocity = playerController.GetScrollingSpeed();
        //print("VELOCITA "+ScrollVelocity);
        rb.velocity = new Vector2(-scrollVelocity * factorProportionalSpeed, rb.velocity.y);
    }
    public virtual void OnObjectSpawn()
    {
        SetBackgroundVelocity();
    }

    public abstract void OnEnemyDeathAnimation();
    
    public RandomSingleton Random1 => random;

    public Rigidbody2D Rb1 => rb;

    public PlayerController PlayerController1 => playerController;

    public float ScrollVelocity1 => scrollVelocity;

    public ObjectPoolSpawner ObjectPoolSpawner => objectPoolSpawner;

    public GameObject Player => player;

    public float InfectProbability => infectProbability;

    public float FactorProportionalSpeed => factorProportionalSpeed;
}
