using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COVIDEnemy : AbstractEnemy
{
    [SerializeField] private float animationRate = 15F;
    [SerializeField] private float animationBounds = 10F;
    
    private float nextAnimationTime = 0f;
    private int nextAxis = 1;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        AnimationSimulation();
    }
    
    private void AnimationSimulation()
    {
        if (Time.time > nextAnimationTime)
        {
            float rand = ((float) Random.NextDouble() + 1f) / 2f;
            Rb.velocity = new Vector2(Rb.velocity.x, nextAxis * rand * animationBounds);
            nextAxis = -1 * nextAxis;
            nextAnimationTime = Time.time + animationRate/100f;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //People Collision
        if (other.gameObject.CompareTag("People"))
        {
            //TODO contagiar personas
            print("Toque a persona");
        }
    }
}
