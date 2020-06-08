using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceReference : ScrollingVelocity
{
    private float distance;
    protected override void Update()
    {
        SetPlayerDistance();
        SetBackgroundVelocity();
    }
    private void SetPlayerDistance()
    {
        distance = transform.position.x * -1f;
        //Update player distance
        PlayerController.DistanceTraveled = distance;
    }

    public float Distance => distance;
}
