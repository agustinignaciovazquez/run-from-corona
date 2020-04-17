using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AbstractEnemy : MonoBehaviour
{
    protected Random Random;
    protected Rigidbody2D Rb;
    protected PlayerController PlayerController;
    protected float ScrollVelocity;
    
    [SerializeField] private GameObject player;
    [SerializeField] private float infectProbability = 0.99f;
    [SerializeField] private float factorProportionalSpeed = 0.9f;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(infectProbability < 0 || infectProbability > 1 || factorProportionalSpeed < 0)
            throw new ArgumentException();
        
        Random = new Random();
        Rb = GetComponent<Rigidbody2D>();
        PlayerController = player.GetComponent<PlayerController>();
        
        SetBackgroundVelocity();
    }
    
    void SetBackgroundVelocity()
    {
        ScrollVelocity = PlayerController.GetScrollingSpeed();
        Rb.velocity = new Vector2(-ScrollVelocity * factorProportionalSpeed, 0f);
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        SetBackgroundVelocity();
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
            //Delete bullet
            other.gameObject.SetActive(false);
            //TODO Create animation here
            print("DESINFECTED");
            //Auto delete ourselves
            this.gameObject.SetActive(false);
        }
        
        //Outbounds
        if (other.gameObject.CompareTag("Outbounds"))
        {
            //Auto delete ourselves
            print("OUTBOUNDS");
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
}
