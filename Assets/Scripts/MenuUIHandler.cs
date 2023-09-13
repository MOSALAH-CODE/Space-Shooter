using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    /// <summary>
    /// Load 2D scene on button click
    /// </summary>
    public void Start2D()
    {
        GameSceneManager.SwitchScene(GameSceneManager._2dSceneName);
    }
    /// <summary>
    /// Load 3D scene on button click
    /// </summary>
    public void Start3D()
    {
        GameSceneManager.SwitchScene(GameSceneManager._3dSceneName);
    }
    /// <summary>
    /// To exit the game
    /// Not working in editor but working in apk
    /// </summary>
    public void ExitTheGame()
    {
        Application.Quit();
    }

}
