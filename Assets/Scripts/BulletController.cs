using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletSpeed = 35.0f;
    /// <summary>
    /// To throw player fire towards the top of the screen and destroy it when it's out of screen range
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);

        if (GameSceneManager.Check2D && transform.position.y > 25.5f)
            Destroy(gameObject);
        else if (GameSceneManager.Check3D && transform.position.z > 25.5f)
            Destroy(gameObject);
    }
}
