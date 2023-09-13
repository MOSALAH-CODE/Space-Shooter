using UnityEngine.SceneManagement;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;    
    }
    /// <summary>
    /// Scroll the background until it reaches the minimum position, then set its position to the starting position
    /// </summary>
    void LateUpdate()
    {
        if ((GameSceneManager.Check2D && !MoveForwardManager.Instance.stopMoving) || SceneManager.GetActiveScene().name == GameSceneManager._menuSceneName)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y < -13f)
                transform.position = startPos;

        }
        else if (GameSceneManager.Check3D && !MoveForwardManager.Instance.stopMoving)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            if (transform.position.z < -13f)
                transform.position = new Vector3(transform.position.x, 0, 2.2f);
        }
    }
}