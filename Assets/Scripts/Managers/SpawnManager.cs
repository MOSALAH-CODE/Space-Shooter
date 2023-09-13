using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// Bomb object to keep the next spawn max and min time and spawn time interval.
    /// </summary>
    private float nextSpawnBomb = 0;
    public int SpownRateBomb = 7;
    public int MinSpownRateBomb = 4;
    /// <summary>
    /// To decrease the spawn interval by time and to keep the next reduction start time.
    /// </summary>
    private float startTime;
    float currTime;

    public GameObject[] SmallStones;
    public static SpawnManager instance = null;

    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SpawnManager").AddComponent<SpawnManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnObject(delayAndSpawnRate));
    }
    /// <summary>
    /// The master object count is used to hold the number of object Prefabs and object Prefabs.
    /// </summary>
    [Serializable]
    public struct Pool
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
            Features.SetRandomPosition(ref obj, y:25.5f);
        else if (GameSceneManager.Check3D)
            Features.SetRandomPosition(ref obj, z:26.5f);
        return obj;
    }


    float delayAndSpawnRate = 1.25f;
    float timeUntilSpawnRateIncrease = 10;

    /// <summary>
    /// According to the specified time interval, it randomly selects and calls the object.
    /// </summary>
    /// <param name="firstDelay">Spawn interval time.</param>
    /// <returns></returns>
    IEnumerator SpawnObject(float firstDelay)
    {
        float spawnRateCountdown = timeUntilSpawnRateIncrease;
        float spawnCountdown = firstDelay;

        while (true)
        {
            yield return null;

            if (EnemyManager.Instance.StopSpawning == false)
            {
                currTime = Time.time;
                //Decreases the bomb spawn interval over time.
                if (currTime - startTime > 10 &&  SpownRateBomb > MinSpownRateBomb)
                {
                    startTime = Time.time;
                    SpownRateBomb--;
                }

                spawnRateCountdown -= Time.deltaTime;
                spawnCountdown -= Time.deltaTime;

                if (spawnCountdown < 0)
                {
                    spawnCountdown += delayAndSpawnRate;
                    int Index = UnityEngine.Random.Range(0, pools.Length);
                    if (Index == 0 && Time.time > nextSpawnBomb)
                        nextSpawnBomb = Time.time + SpownRateBomb;
                    else
                        Index = UnityEngine.Random.Range(1, pools.Length);
                    GetPooledObject(Index);
                }

                // Should the spawn rate increase?
                if (spawnRateCountdown < 0 && delayAndSpawnRate > 0.5)
                {
                    spawnRateCountdown += timeUntilSpawnRateIncrease;
                    delayAndSpawnRate -= 0.1f;
                }
            }

        }
    }
    /// <summary>
    /// It is used to spawn small objects when some large objects explode.
    /// </summary>
    /// <param name="spawnPos">Spawn position is held.</param>
    public void SpawnRandom(Vector3 spawnPos)
    {
        for (int i = 0; i < UnityEngine.Random.Range(0, 6); i++)
        {
            int Index = UnityEngine.Random.Range(0, SmallStones.Length);
            float RandomX = UnityEngine.Random.Range(-1, 2);
            float RandomZ = UnityEngine.Random.Range(-1, 2);
            spawnPos = spawnPos + new Vector3(RandomX, 0, RandomZ);
            Instantiate(SmallStones[Index], spawnPos, SmallStones[Index].transform.rotation);
        }

    }
}
