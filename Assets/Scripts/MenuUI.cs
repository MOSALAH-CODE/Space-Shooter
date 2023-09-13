using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject resumeButton;


    private void Start()
    {
        Menu.SetActive(false);
    }
    /// <summary>
    /// Opening a menu window and stopping the game.
    /// </summary>
    public void Pause ()
    {
        Menu.SetActive(true);
        GameSceneManager.Pause();
    }
    /// <summary>
    /// Closing the menu window and continuing the game.
    /// </summary>
    public void Resume()
    {
        if (GameManager.Instance.gameState == "playing")
            resumeButton.SetActive(true);
        else if (GameManager.Instance.gameState == "victory" || GameManager.Instance.gameState == "lose")
            resumeButton.SetActive(false);
        Menu.SetActive(false);
        GameSceneManager.Resume();
    }
    /// <summary>
    /// Allows you to cross the main stage.
    /// </summary>
    public void Home()
    {
        GameSceneManager.SwitchScene(GameSceneManager._menuSceneName);
        GameSceneManager.Resume();
        GameManager.Instance.gameState = "playing";
    }
    /// <summary>
    /// Found scene restart.
    /// </summary>
    public void RestartGame()
    {
        Menu.SetActive(false);
        GameSceneManager.SwitchScene(GameSceneManager.CurrentScene);
        GameSceneManager.Resume();

    }
}
