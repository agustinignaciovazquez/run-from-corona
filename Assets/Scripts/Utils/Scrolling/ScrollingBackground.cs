using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : ScrollingVelocity
{
    private float backgroundSize;
    private float backgroundLimitOffset;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        backgroundSize = GetComponent<SpriteRenderer>().size.x; 
        backgroundLimitOffset = backgroundSize;
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Transform backgroundTransform = transform;
        Vector3 backGroundPosition = backgroundTransform.position;
        if (backGroundPosition.x + backgroundLimitOffset < 0)
        {
            RepositionBackground(backgroundTransform);
        }
    }
    
    private void RepositionBackground(Transform backgroundTransform)
    {
        Vector2 groundOffset = new Vector2(backgroundSize * 2.0f,0);
        Vector2 newBackgroundPosition = (Vector2) backgroundTransform.position + groundOffset;
        backgroundTransform.position = newBackgroundPosition;
    }
}
