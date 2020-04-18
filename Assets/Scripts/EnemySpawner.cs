using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private List<SpawnItem> itemsToSpawn;
    
    [System.Serializable]
    public class SpawnItem {
        [SerializeField] private GameObject spawnObject;
        [SerializeField] private float spawnProbability;
        [SerializeField] private RangeNum rangeN;
        [SerializeField] private RangeNum rangeX;
        [SerializeField] private RangeNum rangeY;

        SpawnItem()
        {
            //TODO ADD VERIFICATION FOR RANGE N AND PROBABILITY
        }

        public string Tag => spawnObject.tag;
        public float SpawnProbability => spawnProbability;
        public RangeNum RangeN => rangeN;
        public RangeNum RangeX => rangeX;
        public RangeNum RangeY => rangeY;
    }
    
    [System.Serializable]
    public class RangeNum {
        [SerializeField] private float minNum;
        [SerializeField] private float maxNum;
        
        public float MinNum => minNum;
        public float MaxNum => maxNum;
    }
    
    [SerializeField] private float spawnRate = 13F;
    
    private Random random;
    private float nextSpawn = 0.0F;
    private ObjectPoolSpawner objectPoolSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        random = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO DO THIS BY DISTANCE 
        if(Time.time > nextSpawn)
        {
            SpawnItems();
            nextSpawn = Time.time + spawnRate;
        }
    }

    void SpawnItems()
    {
        foreach(SpawnItem item in itemsToSpawn)
        {
            if (RollDice(item.SpawnProbability))
            {
                SpawnPattern(item);
            }
        }
    }
    void SpawnPattern(SpawnItem spawnItem)
    {
        Transform transform1 = this.transform;
        Vector2 position = transform1.position;
        int n = (int) RandomBetween(spawnItem.RangeN.MinNum, spawnItem.RangeN.MaxNum);
        for (int i = 0; i < n; i++)
        {
            float x = (float) RandomBetween(spawnItem.RangeX.MinNum, spawnItem.RangeX.MaxNum);
            float y = (float) RandomBetween(spawnItem.RangeY.MinNum, spawnItem.RangeY.MaxNum);
            Vector2 p = new Vector2(position.x + x, position.y + y);
           objectPoolSpawner.SpawnObject(spawnItem.Tag, p, transform1.rotation);
        }
    }
    
    private double RandomBetween(double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }
    
    private bool RollDice(float spawnPercentage)
    {
        var randomValue = random.NextDouble();
        if (randomValue < spawnPercentage)
        {
            return true;
        }

        return false;
    }
}
