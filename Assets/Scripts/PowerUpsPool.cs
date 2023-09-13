using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsPool : MonoBehaviour
{
    public float minSpawnRate, maxSpawnRate;   

    void Start()
    {
        StartCoroutine(SpawnObject(minSpawnRate));
    }
    /// <summary>
    /// The master object count is used to hold the number of object Prefabs and object Prefabs.
    /// </summary>
    [Serializable] public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int PoolSiz;
    }

    [SerializeField] private Pool[] pools = null;
    /// <summary>
    /// It produces objects using the given objects and the number of objects and stores them in the Queue.
    /// </summary>
    public void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].PoolSiz; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab, transform);
                obj.SetActive(false);
                pools[j].pooledObjects.Enqueue(obj);
            }

        }
    }
    /// <summary>
    /// It brings the selected object and leaves the selected random position as well.
    /// </summary>
    /// <param name="objectType">The selected object number.</param>
    /// <returns></returns>
    public GameObject GetPooledObject(int objectType)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();

        obj.SetActive(true);

        pools[objectType].pooledObjects.Enqueue(obj);
        if (GameSceneManager.Check2D)
            Features.SetRandomPosition(ref obj, y: 25.5f);
        else if (GameSceneManager.Check3D)
            Features.SetRandomPosition(ref obj, z: 26.5f);
        return obj;
    }

    int prevIndex = -1;
    /// <summary>
    /// According to the specified time interval, it randomly selects and calls the object.
    /// </summary>
    /// <param name="firstDelay">Spawn interval time.</param>
    /// <returns></returns>
    IEnumerator SpawnObject(float firstDelay)
    {
        while (true)
        {
            yield return null;

            firstDelay -= Time.deltaTime;

            if (firstDelay < 0)
            {
                firstDelay += UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);
                int index;
                do index = UnityEngine.Random.Range(0, pools.Length);
                while (index == prevIndex);
                prevIndex = index;
                GetPooledObject(index);
            }
        }
        
    }
}
