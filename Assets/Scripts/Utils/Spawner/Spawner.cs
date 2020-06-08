using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnItem {
        [SerializeField] private GameObject spawnObject;
        [SerializeField] private float spawnProbability;
        [SerializeField] private float stepX;
        [SerializeField] private float stepY;
        [SerializeField] private RangeNum rangeN;
        [SerializeField] private RangeNum rangeX;
        [SerializeField] private RangeNum rangeY;
        [SerializeField] private RangeNum paddingY;
        [SerializeField] private RangeNum rangeScale;
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
        public RangeNum PaddingY => paddingY;
        public RangeNum RangeScale => rangeScale;
    }
    
    [System.Serializable]
    public class RangeNum {
        [SerializeField] private float minNum;
        [SerializeField] private float maxNum;
        
        public float MinNum => minNum;
        public float MaxNum => maxNum;
    }
    [SerializeField] private float spawnDistance = 25f;
    [SerializeField] private DistanceReference distanceReference;
    [SerializeField] private List<SpawnItem> itemsToSpawn;
    
    private PlayerController playerController;
    private float nextSpawn = 0.0F;
    private static RandomSingleton _random;
    private static ObjectPoolSpawner _objectPoolSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = distanceReference.PlayerController;
        _objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        _random = RandomSingleton.GetSharedInstance;
        CheckParametersAndSort();
    }

    private void CheckParametersAndSort()
    {
        float total = 0;
        foreach (SpawnItem item in itemsToSpawn)
        {
            total += item.SpawnProbability();
        }
        if (total > 1f)
            throw new ArgumentException("Spawn probability > 1");
        
        //Sort items for algorithm
        itemsToSpawn.Sort((i1,i2)=>
            i1.SpawnProbability().CompareTo(i2.SpawnProbability()));
    }
    
    // Update is called once per frame
    void Update()
    {
        float distance = distanceReference.Distance;
        distance = (distance < 0)? 0 : distance;
        GameObject self = this.gameObject;
        if(distance >= nextSpawn)
        {
            SpawnItem item = SelectRandomItem(self, itemsToSpawn);
            if(item != null)
                SpawnPattern(self, item);
            nextSpawn = distance + spawnDistance;
        }
    }

    SpawnItem SelectRandomItem(GameObject self, List<SpawnItem> itemsToSpawn)
    {
        double rand = _random.NextDouble();
        float percentage = 0f;
        foreach (SpawnItem item in itemsToSpawn)
        {
            if (item.SpawnProbability() > (rand - percentage))
                return item;
            percentage += item.SpawnProbability();
        }

        return null;
    }
    void SpawnItems(GameObject self, List<SpawnItem> itemsToSpawn)
    {
       
        foreach(SpawnItem item in itemsToSpawn)
        {
            if (_random.RollDice(item.SpawnProbability()))
            {
                SpawnPattern(self, item);
            }
        }
    }
    void SpawnPattern(GameObject self, SpawnItem spawnItem)
    {
        Transform transform1 = self.transform;
        int n = (int) _random.RandomBetween(spawnItem.RangeN().MinNum, spawnItem.RangeN().MaxNum);
        float stepX = 0;
        float stepY = 0;
        float paddingY = (float) _random.RandomBetween(spawnItem.PaddingY.MinNum, spawnItem.PaddingY.MaxNum);
        for (int i = 0; i < n; i++)
        {
            //Random positioner
            Vector2 p = GetRandomPositionVector(spawnItem, transform1.position, stepX, paddingY + stepY);
            
            //Random scale
            Vector2 scale = _random.GetRandomVector(spawnItem.RangeScale.MinNum, spawnItem.RangeScale.MaxNum);
            //If scale equals zero we default to the natural scale
            if (scale == Vector2.zero)
                _objectPoolSpawner.SpawnObject(spawnItem.Tag, p);
            else
                _objectPoolSpawner.SpawnObject(spawnItem.Tag, p, scale);
            
            stepX += spawnItem.StepX;
            stepY += spawnItem.StepY;
        }
    }
     Vector2 GetRandomPositionVector(SpawnItem spawnItem, Vector2 position, float stepX, float stepY)
    {
        float x = (float) _random.RandomBetween(spawnItem.RangeX.MinNum, spawnItem.RangeX.MaxNum);
        float y = (float) _random.RandomBetween(spawnItem.RangeY.MinNum, spawnItem.RangeY.MaxNum);
        return new Vector2(position.x + x + stepX + 2 * playerController.ScrollingSpeed , position.y + y + stepY); 
    }
}
