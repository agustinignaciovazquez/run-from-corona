using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingVelocity : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float velocityToPlayerRate = 1f;
    
    private Rigidbody2D rb;
    private float scrollVelocity;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        SetBackgroundVelocity();
    }

    protected virtual void Update()
    {
        SetBackgroundVelocity();
    }
    protected void SetBackgroundVelocity()
    {
        scrollVelocity = playerController.GetScrollingSpeed() * velocityToPlayerRate;
        rb.velocity = new Vector2(-scrollVelocity, 0f);
    }

    public GameObject Player => player;

    public float VelocityToPlayerRate => velocityToPlayerRate;

    public Rigidbody2D Rb => rb;

    public float ScrollVelocity => scrollVelocity;

    public PlayerController PlayerController => playerController;
}