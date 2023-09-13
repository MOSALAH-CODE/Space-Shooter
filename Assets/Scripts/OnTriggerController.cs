using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerController : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;

    int randomTargetPos;

    private void Start()
    {
        if (Features.CheckTags(gameObject, Tags.Enemy, Tags.BossEnemy))
        {
            healthBar.SetMaxHealth(health);
            if (gameObject.GetComponent<Enemy>() != null)
                randomTargetPos = gameObject.GetComponent<Enemy>().randomTargetPos;
        }
    }
    /// <summary>
    /// For 2D and 3D:
    /// At first, the value looks at the object tag and sees if it's shield. If it's not shield, 
    /// then that object tag checks if it's a meteor or EnemyRocket, 
    /// and if it's not, it checks if it's Enemy and does whatever it takes.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ShieldObj))
        {
            gameObject.SetActive(false);
        }
        else if (Features.CheckTags(gameObject, Tags.Missile, Tags.Meteor, Tags.EnemyRocket, Tags.EnemyFire))
        {
            if (collision.gameObject.CompareTag(Tags.Laser))
            {
                StartCoroutine(ExplosionManager.Instance.LaserCollision(collision.gameObject.transform.position));
                Destroy(collision.gameObject);
                health--;
                if (gameObject != null)
                {
                    if (health <= 0)
                        StartCoroutine(ExplosionManager.Instance.GeneralExplosion(gameObject));
                }
            }
        }
        else if (Features.CheckTags(gameObject, Tags.Enemy, Tags.BossEnemy))
        {
            StartCoroutine(ExplosionManager.Instance.LaserCollision(collision.gameObject.transform.position));
            Destroy(collision.gameObject);
            health--;
            healthBar.SetHealth(health);

            if (health <= 0)
            {
                if (gameObject.CompareTag(Tags.BossEnemy))
                    StartCoroutine(ExplosionManager.Instance.BossEnemyExplosion(gameObject));
                else
                    StartCoroutine(ExplosionManager.Instance.EnemyExpolision(randomTargetPos, gameObject, destoy: true));
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag(Tags.ShieldObj))
        {
            gameObject.SetActive(false);
        }
        else if (Features.CheckTags(gameObject, Tags.Stones, Tags.Bomb, Tags.Stones1, Tags.SmallStones, Tags.EnemyRocket, Tags.BigMonster))
        {
            if (collision.gameObject.CompareTag(Tags.Bullet))
            {
                Destroy(collision.gameObject);
                health--;
                if (gameObject != null)
                {
                    if (health <= 0)
                    {
                        StartCoroutine(ExplosionManager.Instance.GeneralExplosion(gameObject));
                        if (gameObject.CompareTag(Tags.Stones1))
                            SpawnManager.Instance.SpawnRandom(gameObject.transform.position);
                    }
                }
            }
        }
        else if (Features.CheckTags(gameObject, Tags.Enemy, Tags.BossEnemy))
        {
            Destroy(collision.gameObject);
            health--;
            healthBar.SetHealth(health);

            if (health <= 0)
            {
                if (gameObject.CompareTag(Tags.BossEnemy))
                    StartCoroutine(ExplosionManager.Instance.BossEnemyExplosion(gameObject));
                else
                    StartCoroutine(ExplosionManager.Instance.EnemyExpolision(randomTargetPos, gameObject, destoy: true));
            }
        }
    }
}
