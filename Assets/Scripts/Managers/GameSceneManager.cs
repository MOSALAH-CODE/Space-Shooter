using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public static bool Check2D;
    public static bool Check3D;

    public static string _menuSceneName = "MenuPortrait";
    public static string _2dSceneName = "2D_Game_Portrait";
    public static string _3dSceneName = "3D_Game_Portrait";
    public static string CurrentScene ;

    /// <summary>
    /// To load a specific scene and update 2d and 3d boolean values.
    /// </summary>
    /// <param name="sceneName">Scene name we want to load it</param>
    public static void SwitchScene(string sceneName)
    {
        if (sceneName.Contains("2D"))
        {
            Check2D = true;
            Check3D = false;
        }
        else if (sceneName.Contains("3D"))
        {
            Check3D = true;
            Check2D = false;
        }
        SceneManager.LoadScene(sceneName);
        CurrentScene = sceneName;
    }
    /// <summary>
    /// To resume game scale time.
    /// </summary>
    public static void Resume()
    {
        Time.timeScale = 1f;
    }
    /// <summary>
    /// To pause game scale time.
    /// </summary>
    public static void Pause()
    {
        Time.timeScale = 0f;
    }

}
