using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public GameObject BulletObject;

    public bool enemyArrived = false;
    /// <summary>
    /// Enemy spawn positions
    /// </summary>
    private float[] spawnPos = { -5, -2.5f, 0, 2.5f, 5 };

    public int randomTargetPos;
    /// <summary>
    /// to spawn the enemy in an available random position
    /// </summary>
    private void Start()
    {
        do randomTargetPos = Random.Range(0, 5); 
        while (EnemyManager.Instance.spawnPosState[randomTargetPos] == true);
        EnemyManager.Instance.spawnPosState[randomTargetPos] = true;
    }

    bool startFire = true;
    /// <summary>
    /// To move the enemy to the randomly selected position.
    /// After the enemy reaches the location, the enemy will start shooting
    /// </summary>
    private void Update()
    {
        if ((GameSceneManager.Check2D && transform.position.y > 15f ) || (GameSceneManager.Check3D && transform.position.z > 15f ))
        {
            if (GameSceneManager.Check3D)
                transform.position += new Vector3(0, 0, Time.deltaTime * -speed);
            else if (GameSceneManager.Check2D)
                transform.position += new Vector3(0, Time.deltaTime * -speed);
        }
        else if (!enemyArrived)
        {
            if (spawnPos[randomTargetPos] < 0 )
            {
                if (transform.position.x > spawnPos[randomTargetPos])
                    transform.position += new Vector3(Time.deltaTime * -speed, 0);
                else if (!enemyArrived)
                    enemyArrived = true;
            }
            else if(transform.position.x < spawnPos[randomTargetPos])
                    transform.position += new Vector3(Time.deltaTime * speed, 0);
            else if (!enemyArrived)
                enemyArrived = true;
        } else if (enemyArrived & startFire)
        {
            StartCoroutine(Fire());
            startFire = false;
        }
    }
    /// <summary>
    /// Let the enemy fire after each randomly selected time delay
    /// </summary>
    /// <returns></returns>
    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2f, 3));
            if (GameSceneManager.Check2D)
                Instantiate(BulletObject, transform.position, transform.GetChild(1).gameObject.transform.rotation);
            else if(GameSceneManager.Check3D)
                Instantiate(BulletObject, transform.position, transform.rotation);
        }
    }    
}
