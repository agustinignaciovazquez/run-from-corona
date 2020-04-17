using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, ObjectPoolInterface
{
    //private Rigidbody2D rb;
    private Rigidbody2D rb;
    private Collider2D coll;

    
    [SerializeField] private float damage = 40f;
    [SerializeField] private float bulletSpeed = 10f;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectSpawn()
    {
        rb.velocity = new Vector2(   bulletSpeed, rb.velocity.y);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Outbounds
        if (other.gameObject.CompareTag("Outbounds"))
        {
            //Auto delete ourselves
            print("OUTBOUNDS");
            this.gameObject.SetActive(false);
        }
    }
}
