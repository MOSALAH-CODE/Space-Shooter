using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    private string Scene;
    void Start()
    {
        BG();
        Scene = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != Scene)
        {
            Scene = SceneManager.GetActiveScene().name;
            BG();
        }
    }
    /// <summary>
    /// When moving to the 3D scene, the object is rotated 90 degrees on the x-axis and reset when it leaves the 3D scene.
    /// </summary>
    void BG()
    {
        if (SceneManager.GetActiveScene().name == GameSceneManager._3dSceneName)
            transform.Rotate(new Vector3(90, 0, 0));
        else
            transform.rotation = new Quaternion(0, 0, 0, 1);
    }
}
