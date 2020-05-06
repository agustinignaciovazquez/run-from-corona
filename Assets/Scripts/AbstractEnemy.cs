using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AbstractEnemy : MonoBehaviour, ObjectPoolInterface
{
    protected Random Random;
    protected Rigidbody2D Rb;
    protected PlayerController PlayerController;
    protected float ScrollVelocity;
    
    [SerializeField] private GameObject player;
    [SerializeField] private float infectProbability = 0.99f;
    [SerializeField] private float factorProportionalSpeed = 0.9f;
   
    private ObjectPoolSpawner objectPoolSpawner;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //Check args are correct
        if(infectProbability < 0 || infectProbability > 1 || factorProportionalSpeed < 0)
            throw new ArgumentException();
        
        //Initializate variables
        Random = new Random();
        Rb = GetComponent<Rigidbody2D>();
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
        Rb.velocity = new Vector2(-ScrollVelocity * factorProportionalSpeed, Rb.velocity.y);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Player Collision
        if (other.gameObject.CompareTag(player.tag))
        {
            var infectionPlayerProbability = InfectPlayerProbability();
            if(InfectionRollDice(infectionPlayerProbability))
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
            var transform1 = this.transform;
            var scale = transform1.localScale;
            //Delete bullet
            other.gameObject.SetActive(false);
            
            //Animate explosion
            objectPoolSpawner.SpawnObject("Green Explosion", transform1.position, new Vector3(scale.x/2,scale.y/2,scale.z/2));
            
            print("DESINFECTED");
            
            //Auto delete ourselves
            this.gameObject.SetActive(false);
        }
    }
    
    private bool InfectionRollDice(float infectionPercentage)
    {
        var randomValue = Random.NextDouble();
        if (randomValue < infectionPercentage)
        {
            return true;
        }

        return false;
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
}
