using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    
    //Game variables
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float endlessSpeed = 3f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 50f;
    [SerializeField] private float normalizeRotationSpeed = 0.5f;
    //FSM
    private enum State
    {
        idle,
        running,
        flying
    };
    private State state = State.idle;

    private int directionTrigger = 0;
    private bool flyTrigger = false;
    
    //private static readonly int Walking = Animator.StringToHash("Walking");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponentInChildren<Collider2D>();
    }  
    

    // Update is called once per frame
    void Update()
    {
        directionTrigger = (int)Input.GetAxis("Horizontal");
        flyTrigger = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        SetPlayerMovement();
        SetPlayerState();
        
    }

    private void SetPlayerMovement()
    {
        //Set player Velocity
        rb.velocity = new Vector2( directionTrigger * moveSpeed + endlessSpeed, rb.velocity.y);
        
        if (flyTrigger)
        {
            print("JUMP");
            if (!coll.IsTouchingLayers(ground))
            {
                //Player not touching the ground, we can rotate according to movement
                Vector3 playerRotation = new Vector3(0,0,-directionTrigger * rotationSpeed);
                transform.Rotate(playerRotation);
            }else
            {
                print("GROUND");
            }
            //Jetpack Force according to rotation and others
            rb.AddForce( transform.rotation * Vector2.up * jetpackForce);
        }
        
        
        //Normalize the rotation
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.identity, Time.deltaTime * normalizeRotationSpeed);
    }
    
    private void SetPlayerState()
    {
        
    } 
    
}
