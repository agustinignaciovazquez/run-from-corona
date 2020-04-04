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
    [SerializeField] private LayerMask roof;
    //Game variables
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float scrollingSpeed = 6f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 40;
    [SerializeField] private float normalizeRotationSpeed = 0.5f;
    //FSM
    private enum State
    {
        Running,
        Flying,
        Falling,
    };
    private State state = State.Running;

    private int directionTrigger = 0;
    private bool flyTrigger = false;
    private static readonly int StateAnimId = Animator.StringToHash("State");

    //private static readonly int Walking = Animator.StringToHash("Walking");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }  
    

    // Update is called once per frame
    void Update()
    {
        directionTrigger = (int)Input.GetAxis("Horizontal");
        flyTrigger = Input.GetButton("Jump");
        SetPlayerState();
    }

    void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetPlayerMovement()
    {
        
        if (flyTrigger)
        {
            if (!coll.IsTouchingLayers(ground))
            {
                //Player not touching the ground, we can rotate according to movement
                Vector3 playerRotation = new Vector3(0,0,-directionTrigger * rotationSpeed);
                transform.Rotate(playerRotation);
                //Player not touching ground, we can move to the sides
                //Set player Velocity
                rb.velocity = new Vector2( directionTrigger * moveSpeed, rb.velocity.y);
            }
            //Jetpack Force according to rotation and others
            rb.AddForce( transform.rotation * Vector2.up * jetpackForce);
        }
        
        
        //Normalize the rotation
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.identity, Time.deltaTime * normalizeRotationSpeed);
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

    public float getScrollingSpeed()
    {
        return scrollingSpeed;
    }
}
