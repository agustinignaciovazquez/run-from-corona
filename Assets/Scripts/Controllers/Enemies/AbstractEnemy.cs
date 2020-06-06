using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, ObjectPoolInterface
{
    private RandomSingleton Random;
    private Rigidbody2D Rb;
    private PlayerController PlayerController;
    private float ScrollVelocity;
    private ObjectPoolSpawner objectPoolSpawner;
    private GameObject player;
    
    [SerializeField] private float infectProbability = 0.99f;
    [SerializeField] private float factorProportionalSpeed = 0.9f;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //Check args are correct
        if(infectProbability < 0 || infectProbability > 1 || factorProportionalSpeed < 0)
            throw new ArgumentException();
        
        //Initializate variables
        Random = RandomSingleton.GetSharedInstance;
        Rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerController = player.GetComponent<PlayerController>();
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        //Update velocity acording to player every frame
        SetBackgroundVelocity();
    }
    void SetBackgroundVelocity()
    {
        ScrollVelocity = PlayerController.GetScrollingSpeed();
        print("VELOCITA "+ScrollVelocity);
        Rb.velocity = new Vector2(-ScrollVelocity * factorProportionalSpeed, Rb.velocity.y);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Player Collision
        if (other.gameObject.CompareTag(player.tag))
        {
            var infectionPlayerProbability = InfectPlayerProbability();
            if(Random.RollDice(infectionPlayerProbability))
            {
                print("MUERTO");
            }
            else
            {
                print("SAFASTE");
            }
        }
        
        //Desinfect Bullet Collision
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Delete bullet
            other.gameObject.SetActive(false);
            
            //Animate explosion
            OnEnemyDeathAnimation();
            
            print("DESINFECTED");
            
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
        float playerInfectionDefense = PlayerController.GetInfectionDefense() > 0 ? PlayerController.GetInfectionDefense() : 0;
        float infectionProbability = GetInfectProbability() - playerInfectionDefense;
        return infectionProbability > 0 ? infectionProbability : 0;
    }

    public virtual void OnObjectSpawn()
    {
        SetBackgroundVelocity();
    }

    public abstract void OnEnemyDeathAnimation();
    
    public RandomSingleton Random1 => Random;

    public Rigidbody2D Rb1 => Rb;

    public PlayerController PlayerController1 => PlayerController;

    public float ScrollVelocity1 => ScrollVelocity;

    public ObjectPoolSpawner ObjectPoolSpawner => objectPoolSpawner;

    public GameObject Player => player;

    public float InfectProbability => infectProbability;

    public float FactorProportionalSpeed => factorProportionalSpeed;
}
