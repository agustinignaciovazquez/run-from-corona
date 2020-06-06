using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private EnergyBar energyBar;
    //Jetpack Energy Vars
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float energyRegen = 0.5f;
    [SerializeField] private float energySpend = 0.3f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 40f;
    [SerializeField] private float normalizeRotationSpeed = 3f;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private AudioSource audioSource;
    
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody2D>();
        coll = player.GetComponent<Collider2D>();
        
        //currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPlayerMovement();
    }
    
    private void SetPlayerMovement()
    {
        playerTransform = player.transform;
        if (playerController.FlyTrigger)
        {
            ReduceEnergy(energySpend);
            //If player not touching ground we can rotate according to movement
            if (!coll.IsTouchingLayers(playerController.Ground))
            {
                RotateFlyingPlayer();
            }
            //Jetpack Force according to rotation and others
            rb.AddForce( playerTransform.rotation * Vector2.up * jetpackForce);
            
            //TODO Play different sounds based on playerPrefs
            if(!audioSource.isPlaying) {
                audioSource.Play();
            }
            
            
        }
        else
        {
            RegenerateEnergy(energyRegen);
            if(audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
        
        //Normalize the rotation
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation,Quaternion.identity, Time.deltaTime * normalizeRotationSpeed);
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
    private void RotateFlyingPlayer()
    {
        playerTransform = player.transform;
        //Player not touching the ground, we can rotate 
        Vector3 playerRotation = new Vector3(0,0,-playerController.DirectionTrigger * rotationSpeed);
        playerTransform.Rotate(playerRotation);
        //Player not touching ground, we can move to the sides
        //Set player Velocity
        rb.velocity = new Vector2( playerController.DirectionTrigger * moveSpeed, rb.velocity.y);
    }
    
    public float CurrentEnergy => currentEnergy;
}
