using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager instance = null;
    public static ExplosionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ExplosionManager").AddComponent<ExplosionManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }

    public AudioSource sfxExplosion;

    public GameObject ExplosionObject;
    GameObject ExplosionObjectClone;

    public GameObject LaserCollisionObject;
    GameObject LaserCollisionClone;
    void Start()
    {
        ExplosionObject.SetActive(false);
    }
    /// <summary>
    /// To create an explosion at a specific location
    /// </summary>
    /// <param name="Pos">location of the explosion to be created</param>
    public void Explosion(Vector3 Pos)
    {
        sfxExplosion.Play(); 
        ExplosionObjectClone = Instantiate(ExplosionObject, Pos, ExplosionObject.transform.rotation);
        ExplosionObjectClone.SetActive(true);
    }
    /// <summary>
    /// To create a laser collision at a specific location
    /// </summary>
    /// <param name="Pos">location of the laser collision to be created</param>
    public IEnumerator LaserCollision(Vector3 Pos)
    {
        LaserCollisionClone = Instantiate(LaserCollisionObject, Pos, LaserCollisionObject.transform.rotation);
        LaserCollisionClone.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
    }
    /// <summary>
    /// To start enemy explosion 
    /// </summary>
    /// <param name="enemySpawnIndex">to set the enemy's pos status to false because the enemy is dead</param>
    /// <param name="gameObject">To get location of the explosion to be created and to set  enemy's gameObject to false after explosion is finished</param>
    /// <param name="Score">Scores gain after killing the enemy and by default it is set to 2</param>
    /// <param name="destoy">to destroy the enemy or set it's gameObject to false and by default it is set to false</param>
    /// <returns></returns>
    public IEnumerator EnemyExpolision(int enemySpawnIndex, GameObject gameObject, int Score = 2, bool destoy = false)
    {
        EnemyManager.Instance.decrementEnemyNum();
        EnemyManager.Instance.spawnPosState[enemySpawnIndex] = false;
        Singleton.Instance.SetScore(Score);
        Explosion(gameObject.transform.position);
        yield return new WaitForSecondsRealtime(0.1f);
        if (destoy)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
    /// <summary>
    /// To start boss enemy explosion 
    /// </summary>
    /// <param name="gameObject">To get location of the explosion to be created and to set boss enemy's gameObject to false after explosion is finished</param>
    /// <param name="Score">Scores gain after killing the boss enemy and by default it is set to 25</param>
    /// <returns></returns>
    public IEnumerator BossEnemyExplosion(GameObject gameObject, int Score = 25)
    {
        Singleton.Instance.SetScore(Score);
        Explosion(gameObject.transform.position);
        StopCoroutine(EnemyManager.Instance.SpawnEnemiesCoroutine);
        EnemyManager.Instance.StopSpawning = false;
        yield return new WaitForSecondsRealtime(0.1f);
        EnemyManager.Instance.ResetValues();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// To start the meteor explosion.
    /// </summary>
    /// <param name="gameObject">To get location of the explosion to be created</param>
    /// <param name="Score">Meteor gains points after exploding and by default it's 1</param>
    /// <returns></returns>
    public IEnumerator GeneralExplosion(GameObject gameObject, int Score = 1)
    {
        Singleton.Instance.SetScore(Score);
        ExplosionManager.instance.Explosion(gameObject.transform.position);
        yield return new WaitForSecondsRealtime(0.1f);
        gameObject.SetActive(false);
    }
}