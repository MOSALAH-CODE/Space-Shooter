using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("EnemyManager").AddComponent<EnemyManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }

    public GameObject bossEnemy;
    public GameObject enemy;
    public GameObject direction;

    GameObject bossEnemyClone;
    GameObject enemyClone;

    public float BossEnemySpawnDelay = 10f;

    public int CurrentEnemyNum = 0;
    public int maxEnemy = 20;
    public int CountEnemy;

    public int SizeOfEnemySpawnPos = 5;
    public bool[] spawnPosState;

    public bool BossAlive = false;
    public bool StopSpawning = false;

    public Coroutine SpawnEnemiesCoroutine;

    private void Start()
    {
        spawnPosState = new bool[SizeOfEnemySpawnPos];
    }
    /// <summary>
    /// If boss enemy is not alive spawn him after delay
    /// </summary>
    private void Update()
    {
        if (!BossAlive)
        {
            StartCoroutine(SpawnBossEnemy(BossEnemySpawnDelay));
        }
    }
    /// <summary>
    /// Boss enemy spawns after a certain delay
    /// </summary>
    /// <param name="delay">Delay time to spawn</param>
    /// <returns></returns>
    IEnumerator SpawnBossEnemy(float delay = 10f)
    {
        BossAlive = true;
        yield return new WaitForSecondsRealtime(delay);
        InstantiateBossEnemy();
    }
    /// <summary>
    /// To instantiate the boss enemy, then call the spawn enemies coroutine
    /// </summary>
    void InstantiateBossEnemy()
    {
        BossAlive = true;
        StopSpawning = true;
        bossEnemyClone = Instantiate(bossEnemy, transform);

        if (GameSceneManager.Check2D)
            bossEnemyClone.transform.position = new Vector3(0, 30);
        else if (GameSceneManager.Check3D)
            bossEnemyClone.transform.position = new Vector3(0, 1, 30);

        SpawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
    }
    /// <summary>
    /// Wait for the boss enemy to get to his position, then keep spawning the enemies until you reach the maximum number of enemies
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        bool wait = true;
        int x = 0;
        yield return new WaitWhile(() => !bossEnemyClone.GetComponent<BossEnemy>().bossEnemyArrived);

        while (CountEnemy < maxEnemy)
        {
            yield return null;
            if (CurrentEnemyNum < SizeOfEnemySpawnPos && !wait && x < SizeOfEnemySpawnPos)
            {
                x++;
                InstantiateSpawnEnemy();
                yield return new WaitForSecondsRealtime(1f);
                if (x == SizeOfEnemySpawnPos)
                    wait = true;
            }
            else if (wait && CurrentEnemyNum == 0)
            {
                yield return new WaitForSecondsRealtime(10f);
                wait = false;
                x = 0;

            }
        }
    }
    /// <summary>
    /// To instantiate the enemy
    /// </summary>
    void InstantiateSpawnEnemy()
    {
        CurrentEnemyNum++;
        CountEnemy++;
        enemyClone = Instantiate(enemy, transform);

        if (GameSceneManager.Check2D)
            enemyClone.transform.position = new Vector3(0, 20);
        else if (GameSceneManager.Check3D)
            enemyClone.transform.position = new Vector3(0, 1, 20);
    }
    /// <summary>
    /// Reset some values after killing the boss enemy.
    /// </summary>
    public void ResetValues()
    {
        BossAlive = false;
        CurrentEnemyNum = 0;
        CountEnemy = 0;
    }
    /// <summary>
    /// To reduce the number of available enemies when an enemy is killed.
    /// </summary>
    public void decrementEnemyNum()
    {
        if (CurrentEnemyNum > 0)
            CurrentEnemyNum--;
    }
}
