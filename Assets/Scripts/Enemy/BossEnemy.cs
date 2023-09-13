using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public bool bossEnemyArrived = false;
    public bool spawnRockets = true;
    public float BossEnemyPosition = 20;
    private float delayAndSpawnRate = 1.25f;

    
    private Quaternion directionRotation;

    private Coroutine coroutine;
    private void Update()
    {
        directionRotation = EnemyManager.Instance.direction.transform.rotation;

        if (EnemyManager.Instance.CurrentEnemyNum <= 0 && spawnRockets)
        {
            coroutine = StartCoroutine(SpawnObject(delayAndSpawnRate));
            spawnRockets = false;
        }
        else if (EnemyManager.Instance.CurrentEnemyNum > 0)
        {
            StopCoroutine(coroutine);
            spawnRockets = true;
        }
        //Sending to the determined position.
        if (GameSceneManager.Check2D && !MoveForwardManager.Instance.stopMoving)
        {
            if (transform.position.y > BossEnemyPosition)
                transform.position += new Vector3(0, Time.deltaTime * -2);
            else if (!bossEnemyArrived)
                bossEnemyArrived = true;
        }
        else if (GameSceneManager.Check3D && !MoveForwardManager.Instance.stopMoving)
        {
            if (transform.position.z > BossEnemyPosition)
                transform.position += new Vector3(0, 0, Time.deltaTime * -2);
            else if (!bossEnemyArrived)
                bossEnemyArrived = true;
        }
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
                GameObject obj = Instantiate(pools[j].objectPrefab, transform.parent);
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

        obj.SetActive(true); // MissingReferenceException

        if (GameSceneManager.Check2D)
        {
            pools[objectType].pooledObjects.Enqueue(obj);
            Vector3 spawnPos = new Vector3(0, gameObject.transform.position.y);
            obj.transform.rotation = directionRotation;
            obj.transform.position = spawnPos;
        }
        else if (GameSceneManager.Check3D)
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);

            pools[objectType].pooledObjects.Enqueue(obj);
            Vector3 spawnPos = new Vector3(0, 1, gameObject.transform.position.z);
            obj.transform.rotation = directionRotation;
            obj.transform.position = spawnPos;
            
            obj.transform.GetChild(0).gameObject.transform.position = spawnPos;
        }
       
        return obj;
    }
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
                firstDelay += delayAndSpawnRate;
                int Index = UnityEngine.Random.Range(0, pools.Length);
                GetPooledObject(Index);
            }
        }
    }

    
}
