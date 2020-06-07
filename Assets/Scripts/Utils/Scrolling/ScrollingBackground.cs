using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ScrollingBackground : ScrollingVelocity
{
    protected float backgroundSize;
    
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        backgroundSize = GetBackgroundSize(); 
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Transform backgroundTransform = transform;
        Vector3 backGroundPosition = backgroundTransform.position;
        
        if (backGroundPosition.x + backgroundSize < 0)
        {
            RepositionBackground(backgroundTransform);
        }
    }

    protected virtual void RepositionBackground(Transform backgroundTransform)
    {
        Vector2 groundOffset = new Vector2(backgroundSize * 2.0f,0);
        Vector2 newBackgroundPosition = (Vector2) backgroundTransform.position + groundOffset;
        backgroundTransform.position = newBackgroundPosition;
    }

    protected float GetBackgroundSize()
    {
        return GetComponent<SpriteRenderer>().size.x; 
    }
}
