﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingVelocity : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private float velocityToPlayerRate = 1f;
    private Rigidbody2D rb;
    private float scrollVelocity;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SetBackgroundVelocity();
    }

    protected virtual void Update()
    {
        SetBackgroundVelocity();
    }
    
    protected void SetBackgroundVelocity()
    {
        scrollVelocity = playerController.GetBackgroundScrollSpeed() * velocityToPlayerRate;
        rb.velocity = new Vector2(-scrollVelocity, 0f);
    }

    public PlayerController PlayerController => playerController;

    public float VelocityToPlayerRate => velocityToPlayerRate;

    public Rigidbody2D Rb => rb;
    
}