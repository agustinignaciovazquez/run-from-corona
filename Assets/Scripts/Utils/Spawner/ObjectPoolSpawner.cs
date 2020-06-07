using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPoolSpawner : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolItem> itemsToPool;
    
    [System.Serializable]
    public class ObjectPoolItem {
        [SerializeField] private int amountToPool;
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private bool shouldExpand;

        public int AmountToPool => amountToPool;

        public GameObject ObjectToPool => objectToPool;

        public bool ShouldExpand => shouldExpand;
    }
    
    private Dictionary<string, ObjectPoolQueue> pooledObjects;
    private static ObjectPoolSpawner _sharedInstance;
    
    void Awake() {
        _sharedInstance = this;
    }
    
    // Start is called before the first frame update
    void Start () {
        pooledObjects = new Dictionary<string, ObjectPoolQueue>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if(item.ObjectToPool != null)
                AddToDictionary(item.ObjectToPool.tag, item);
        }
    }
    private ObjectPoolQueue AddToDictionary(String tag, ObjectPoolItem obj)
    {
        if (pooledObjects.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+tag+" alredy exists.");
            throw new ArgumentException();
        }

        ObjectPoolQueue objectPoolQueue = new ObjectPoolQueue(obj.AmountToPool, obj.ObjectToPool, obj.ShouldExpand);
        pooledObjects.Add(tag, objectPoolQueue);
        return objectPoolQueue;
    }

    public void ResetPool()
    {
        foreach (string key in pooledObjects.Keys.ToList())
        {
            foreach (GameObject go in pooledObjects[key].ObjectPool)
            {
                go.SetActive(false);
            }
        }
    }
    public GameObject SpawnObject(string tag, Vector2 position, Quaternion? rotation, Vector3? scale) {
        if (!pooledObjects.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+tag+" not found.");
            return null;
        }

        GameObject objectToSpawn = pooledObjects[tag].SpawnObject();
        if (objectToSpawn == null)
        {
            Debug.LogWarning("Pool with tag "+tag+" is full.");
            return null;
        }
        
        objectToSpawn.transform.position = position;
        if(rotation.HasValue)
            objectToSpawn.transform.rotation = rotation.Value;
        if (scale.HasValue)
            objectToSpawn.transform.localScale = scale.Value;

        ObjectPoolInterface objectPoolInterface = objectToSpawn.GetComponent<ObjectPoolInterface>();
        
        objectToSpawn.SetActive(true);
        if(objectPoolInterface != null)
            objectPoolInterface.OnObjectSpawn();
        
        return objectToSpawn;
    }

    public GameObject SpawnObject(string tag, Vector2 position)
    {
        return SpawnObject(tag, position, null, null);
    }
    
    public GameObject SpawnObject(string tag, Vector2 position, Quaternion rotation)
    {
        return SpawnObject(tag, position, rotation, null);
    }
    
    public GameObject SpawnObject(string tag, Vector2 position, Vector3 scale)
    {
        return SpawnObject(tag, position, null, scale);
    }
    
    public static ObjectPoolSpawner GetSharedInstance => _sharedInstance;
    private class ObjectPoolQueue {
        private int amountToPool;
        private Queue<GameObject> objectPool;
        private bool shouldExpand;

        public ObjectPoolQueue(int amountToPool, GameObject go, bool shouldExpand)
        {
            if(amountToPool < 1 || go == null)
                throw new ArgumentException();
            
            this.amountToPool = amountToPool;
            this.objectPool = new Queue<GameObject>();
            this.shouldExpand = shouldExpand;
            FillObjectPool(go);
          
        }
        private void FillObjectPool(GameObject go)
        {
            for (int i = 0; i < AmountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(go);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }
        public GameObject SpawnObject()
        {
            GameObject objectToSpawn = objectPool.Peek();
            if (objectToSpawn != null && objectToSpawn.activeSelf)
            {
                if (!this.shouldExpand)
                {
                    Debug.LogWarning("Pool with tag "+objectToSpawn.tag+" is full.");
                    return null;
                }
                
                GameObject obj = (GameObject)Instantiate(objectToSpawn);
                //obj.SetActive(false);
                this.amountToPool++;
                objectToSpawn = obj;
            }
            else
            {
                objectToSpawn = objectPool.Dequeue();
                //print(objectToSpawn);
            }
            
            objectPool.Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        public Queue<GameObject> ObjectPool => objectPool;
        public int AmountToPool => amountToPool;
        public bool ShouldExpand => shouldExpand;
    }
}
