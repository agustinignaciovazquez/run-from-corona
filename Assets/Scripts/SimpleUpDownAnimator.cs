using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SimpleUpDownAnimator : MonoBehaviour
{
    [SerializeField] private float animationRate = 1.5F;
    [SerializeField] private float animationBounds = 1F;
    
    private float nextAnimationTime = 0f;
    private int nextAxis = 1;
    private Random random;
    private Rigidbody2D rb;
    //TODO IMPLEMENT X MOVEMENT TOO 
    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationSimulation();
    }
    
    private void AnimationSimulation()
    {
        if (Time.time > nextAnimationTime)
        {
            float rand = ((float) random.NextDouble() + 1f) / 2f; // [0.5 - 1 ]
            rb.velocity = new Vector2(rb.velocity.x, nextAxis * rand * animationBounds);
            nextAxis = -1 * nextAxis;
            nextAnimationTime = Time.time + animationRate;
        }
    }
}
