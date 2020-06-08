using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : AbstractEnemy
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //People Collision
        if (other.gameObject.CompareTag("People"))
        {
            //TODO contagiar personas
        }
        if(other.gameObject.CompareTag("Covid") || other.gameObject.CompareTag("CoinPickup") || other.gameObject.CompareTag("PickupBullet")) 
            other.gameObject.SetActive(false);
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
