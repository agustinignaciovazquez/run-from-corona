using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private float scrollVelocity;
    
    private float backgroundSize;
    private float backgroundLimitOffset;
    
    // Start is called before the first frame update
    void Awake()
    {
        backgroundSize = GetComponent<SpriteRenderer>().size.x; 
        backgroundLimitOffset = backgroundSize;
        rb = GetComponent<Rigidbody2D>();
        scrollVelocity = player.GetComponent<PlayerController>().GetScrollingSpeed();
        rb.velocity = new Vector2(-scrollVelocity, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO ADD Difficulty throw time
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
