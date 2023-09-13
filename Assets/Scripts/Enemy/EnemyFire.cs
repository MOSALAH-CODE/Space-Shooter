using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private float Speed = 20;
    /// <summary>
    /// To throw enemy fire down the screen and destroy it when it's out of screen range
    /// </summary>
    void Update()
    {
        if (GameSceneManager.Check2D)
        {
            transform.Translate(Vector3.down * Time.deltaTime * Speed);
            if (transform.position.y < -7f)
                Destroy(gameObject);
        }
        else if (GameSceneManager.Check3D)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            if (transform.position.z < -7f)
                Destroy(gameObject);
        }
    }
}


