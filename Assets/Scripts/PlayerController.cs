using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    private static PlayerController instance = null;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PlayerController").AddComponent<PlayerController>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }
    
    public AudioSource sfxCollision;
    
    public Animator collisionAnim;

    /// <summary>
    /// Starts the collision animation when the player hits any of the obstacles
    /// </summary>
    /// <param name="TAG">Collision animation tag</param>
    /// <returns></returns>
    IEnumerator PlayCollisionAnim(string TAG)
    {
        sfxCollision.Play();
        collisionAnim.SetBool(TAG, true);
        yield return new WaitForSecondsRealtime(0.3f);
        collisionAnim.SetBool(TAG, false);

    }
    /// <summary>
    /// Runs when the player detects the collision of any of the obstacles.
    /// </summary>
    /// <param name="gameObject">obstacle collider</param>
    void TriggerEnter(GameObject gameObject)
    {
        StartCoroutine(PlayCollisionAnim(Tags.collisionAnimation));
        Singleton.Instance.SetHealth(gameObject.tag);
        gameObject.SetActive(false);

        if (Singleton.Instance.PlayerHealth <= 0)
            base.gameObject.SetActive(false);
    }
}
