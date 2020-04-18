using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPoolSpawner objectPoolSpawner;
    [SerializeField] private float spawnRate = 13F;
    private float nextSpawn = 0.0F;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO DO THIS BY DISTANCE 
        if(Time.time > nextSpawn)
        {
            SpawnPattern();
            nextSpawn = Time.time + spawnRate;
        }
    }

    void SpawnPattern()
    {
        var transform1 = this.transform;
        var position = transform1.position;
        for (int i = 1; i < 10; i++)
        {
            Vector2 p = new Vector2(position.x + i, position.y);
            objectPoolSpawner.SpawnObject("Covid", p, transform1.rotation);
        }
       
    }
}
