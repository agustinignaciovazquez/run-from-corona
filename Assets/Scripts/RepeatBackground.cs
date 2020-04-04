using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepeatBackground : MonoBehaviour
{
    private float backgroundSize;
    private float backgroundLimitOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundSize = GetComponent<SpriteRenderer>().size.x; 
        backgroundLimitOffset = backgroundSize;
    }
    
    // Update is called once per frame
    void Update()
    {
        Transform backgroundTransform = transform;
        Vector3 backGroundPosition = backgroundTransform.position;
        if (backGroundPosition.x  + backgroundLimitOffset < 0)
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
