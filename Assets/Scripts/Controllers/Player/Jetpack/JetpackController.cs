using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private AudioSource audioSource;
    
    private PlayerController playerController;
    private PlayerItemsState playerItemsState;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerItemsState = playerController.PlayerItemsState;
        
        rb = player.GetComponent<Rigidbody2D>();
        coll = player.GetComponent<Collider2D>();
        
        //playerItemsState.CurrentEnergy = playerItemsState.CurrentJetpack.MaxEnergy;
        energyBar.SetMaxEnergy(playerItemsState.CurrentJetpack.MaxEnergy);
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
            ReduceEnergy(playerItemsState.CurrentJetpack.EnergySpend);
            //If player not touching ground we can rotate according to movement
            if (!coll.IsTouchingLayers(playerController.Ground))
            {
                RotateFlyingPlayer();
            }
            //Jetpack Force according to rotation and others
            rb.AddForce( playerTransform.rotation * Vector2.up * playerItemsState.CurrentJetpack.JetpackForce);
            
            //TODO Play different sounds based on playerPrefs
            if(!audioSource.isPlaying) {
                audioSource.Play();
            }
            
            
        }
        else
        {
            RegenerateEnergy(playerItemsState.CurrentJetpack.EnergyRegen);
            if(audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
        
        //Normalize the rotation
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation,Quaternion.identity, Time.deltaTime * playerItemsState.CurrentJetpack.NormalizeRotationSpeed);
    }
    
    void RegenerateEnergy(float energyToRegen)
    {
        if (playerItemsState.CurrentEnergy < playerItemsState.CurrentJetpack.MaxEnergy)
        {
            playerItemsState.CurrentEnergy += energyToRegen;
            energyBar.SetEnergy(playerItemsState.CurrentEnergy);
        }
        
    }

    void ReduceEnergy(float energyToLose)
    {
        playerItemsState.CurrentEnergy -= energyToLose;
        energyBar.SetEnergy(playerItemsState.CurrentEnergy);
    }
    private void RotateFlyingPlayer()
    {
        playerTransform = player.transform;
        //Player not touching the ground, we can rotate 
        Vector3 playerRotation = new Vector3(0,0,-playerController.DirectionTrigger * playerItemsState.CurrentJetpack.RotationSpeed);
        playerTransform.Rotate(playerRotation);
        //Player not touching ground, we can move to the sides
        //Set player Velocity
        rb.velocity = new Vector2( playerController.DirectionTrigger * playerItemsState.CurrentJetpack.MoveSpeed, rb.velocity.y);
    }
    
    public float CurrentEnergy => playerItemsState.CurrentEnergy;
}
