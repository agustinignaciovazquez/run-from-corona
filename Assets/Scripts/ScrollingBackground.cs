using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private float scrollVelocity;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scrollVelocity = player.GetComponent<PlayerController>().getScrollingSpeed();
        rb.velocity = new Vector2(-scrollVelocity, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO ADD Difficulty throw time
    }
}
