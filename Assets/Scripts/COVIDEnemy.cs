using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COVIDEnemy : AbstractEnemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
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

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        float randomScale = (float) (Random.NextDouble() + 1f) /2f;
        this.gameObject.transform.localScale = new Vector2(randomScale, randomScale);
    }
}
