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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
    }

    public override void OnEnemyDeathAnimation()
    { 
        var transform1 = this.transform;
        var scale = transform1.localScale;
        ObjectPoolSpawner.SpawnObject("Green Explosion", transform1.position, new Vector3(scale.x/2,scale.y/2,scale.z/2));
    }
}
