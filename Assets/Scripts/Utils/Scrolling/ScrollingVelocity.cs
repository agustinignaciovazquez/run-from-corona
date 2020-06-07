using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingVelocity : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float velocityToPlayerRate = 1f;
    [SerializeField] private bool distanceReference = false;
    private Rigidbody2D rb;
    private float scrollVelocity;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetBackgroundVelocity();
    }

    protected virtual void Update()
    {
        if(distanceReference)
            SetPlayerDistance();
        SetBackgroundVelocity();
    }

    protected void SetPlayerDistance()
    {
        float distance = transform.position.x * -1f;
        //Update player distance
        PlayerController.DistanceTraveled = distance;
    }
    
    protected void SetBackgroundVelocity()
    {
        scrollVelocity = playerController.GetBackgroundScrollSpeed() * velocityToPlayerRate;
        rb.velocity = new Vector2(-scrollVelocity, 0f);
    }

    public PlayerController PlayerController => playerController;

    public float VelocityToPlayerRate => velocityToPlayerRate;

    public Rigidbody2D Rb => rb;
    
}