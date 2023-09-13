using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    /// <summary>
    /// Rotate the Enemy towards the player
    /// </summary>
    private void Update()
    {
        if (GameSceneManager.Check3D)
            transform.LookAt(PlayerController.Instance.gameObject.transform.position);
        else if(GameSceneManager.Check2D)
            Features.LookAt2D(transform, PlayerController.Instance.gameObject.transform.position);
    }
}
