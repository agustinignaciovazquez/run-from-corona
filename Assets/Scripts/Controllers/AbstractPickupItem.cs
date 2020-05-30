using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPickupItem : MonoBehaviour, ObjectPoolInterface
{
    private RandomSingleton random;
    private Rigidbody2D rb;
    private float scrollVelocity;
    private ObjectPoolSpawner objectPoolSpawner;
    private PlayerController playerController;
    
    [SerializeField] private GameObject player;
    
    [SerializeField] private float factorProportionalSpeed = 1.0f;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //Initializate variables
        random = RandomSingleton.GetSharedInstance;
        rb = GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        //Update velocity acording to player every frame
        SetBackgroundVelocity();
    }
    void SetBackgroundVelocity()
    {
        scrollVelocity = playerController.GetScrollingSpeed();
        rb.velocity = new Vector2(-scrollVelocity * factorProportionalSpeed, rb.velocity.y);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Player Collision
        if (other.gameObject.CompareTag(player.tag))
        {
            OnPickup();
            OnPickupAnimation();
            //Auto delete ourselves
            this.gameObject.SetActive(false);
        }
    }
    public virtual void OnObjectSpawn()
    {
        SetBackgroundVelocity();
    }

    public abstract void OnPickup();
    public abstract void OnPickupAnimation();
    
    public RandomSingleton Random => random;

    public Rigidbody2D Rb => rb;

    public PlayerController PlayerController => playerController;

    public float ScrollVelocity => scrollVelocity;

    public ObjectPoolSpawner ObjectPoolSpawner => objectPoolSpawner;

    public GameObject Player => player;
    
}