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
        [SerializeField] private float stepX;
        [SerializeField] private float stepY;
        [SerializeField] private RangeNum rangeN;
        [SerializeField] private RangeNum rangeX;
        [SerializeField] private RangeNum rangeY;
        
        public float SpawnProbability()
        {
            if (spawnProbability < 0f || spawnProbability > 1f)
                throw new ArgumentException();
            return spawnProbability;
        }

        public RangeNum RangeN()
        {
            if (rangeN.MinNum < 0)
                throw new ArgumentException();
            return rangeN;
        }
        
        public string Tag => spawnObject.tag;
        public float StepX => stepX;
        public float StepY => stepY;
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
    
    private static Random _random;
    private float nextSpawn = 0.0F;
    private static ObjectPoolSpawner _objectPoolSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        _objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        _random = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO DO THIS BY DISTANCE 
        GameObject self = this.gameObject;
        if(Time.time > nextSpawn)
        {
            SpawnItems(self, itemsToSpawn);
            nextSpawn = Time.time + spawnRate;
        }
    }

    static void SpawnItems(GameObject self, List<SpawnItem> itemsToSpawn)
    {
       
        foreach(SpawnItem item in itemsToSpawn)
        {
            if (RollDice(item.SpawnProbability()))
            {
                SpawnPattern(self, item);
            }
        }
    }
    static void SpawnPattern(GameObject self, SpawnItem spawnItem)
    {
        Transform transform1 = self.transform;
        Vector2 position = transform1.position;
        int n = (int) RandomBetween(spawnItem.RangeN().MinNum, spawnItem.RangeN().MaxNum);
        float stepX = 0;
        float stepY = 0;
        
        for (int i = 0; i < n; i++)
        {
            float x = (float) RandomBetween(spawnItem.RangeX.MinNum, spawnItem.RangeX.MaxNum);
            float y = (float) RandomBetween(spawnItem.RangeY.MinNum, spawnItem.RangeY.MaxNum);
            Vector2 p = new Vector2(position.x + x + stepX , position.y + y + stepY);
            float randomScale = (float) (_random.NextDouble() + 1f) /2f;
            Vector2 scale = new Vector2(randomScale, randomScale);
            _objectPoolSpawner.SpawnObject(spawnItem.Tag, p, scale);
            stepX += spawnItem.StepX;
            stepY += spawnItem.StepY;
        }
    }
    
    private static double RandomBetween(double min, double max)
    {
        return _random.NextDouble() * (max - min) + min;
    }
    
    private static bool RollDice(float spawnPercentage)
    {
        var randomValue = _random.NextDouble();
        if (randomValue < spawnPercentage)
        {
            return true;
        }

        return false;
    }
}
