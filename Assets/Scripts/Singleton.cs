using System.Collections;
using UnityEngine;
using TMPro;

public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    public int PlayerScore = 0;

    public int ScoreMultiplier = 1;

    public int PlayerHealth = 3;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ScoreText;
    public GameObject GameOverTextObject;
    public GameObject YouWinTextObject;
    public GameObject GameEndMenuObject;
    public GameObject ResumeButtonObject;
    public GameObject PauseButtonObject;
    public GameObject JoystickButtonObject;
    public GameObject FireButtonObject;
    public Transform PlayerPos;
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Singleton").AddComponent<Singleton>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }
    /// <summary>
    /// According to the hit object, the player's health changes.
    /// </summary>
    /// <param name="obstacle">Tag to the hit object.</param>
    public void SetHealth(string obstacle)
    {
        if (Features.CheckStrings(obstacle, Tags.Meteor, Tags.Stones, Tags.Stones1, Tags.SmallStones, Tags.EnemyRocket, Tags.EnemyFire) && PlayerHealth > 0)
            PlayerHealth--;
        else if (Features.CheckStrings(obstacle, Tags.Missile, Tags.Bomb))
            PlayerHealth = 0;
        else if (Features.CheckStrings(obstacle, Tags.Astronaut, Tags.HealingPowerUp))
            PlayerHealth++;
        HealthText.text = PlayerHealth.ToString();

        if (PlayerHealth <= 0)
            StartCoroutine(GameOver());
    }
    /// <summary>
    /// Allows the player to score points according to the exploded object.
    /// </summary>
    /// <param name="Score">Exploding object score.</param>
    public void SetScore(int Score)
    {
        PlayerScore += Score * ScoreMultiplier;
        ScoreText.text = PlayerScore.ToString();
        if (PlayerScore >= 1000)
        {
            GameManager.Instance.gameState = "victory";
            Features.ActiveteAll(GameEndMenuObject, YouWinTextObject);
            Features.DeactiveteAll(PauseButtonObject, ResumeButtonObject, JoystickButtonObject, FireButtonObject);
            GameSceneManager.Pause();
        }
    }
    /// <summary>
    /// When losing the game, the game over window allows to appear.
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        GameManager.Instance.gameState = "lose";
        MoveForwardManager.Instance.stopMoving = true;
        Features.DeactiveteAll(ResumeButtonObject, JoystickButtonObject, FireButtonObject);
        ExplosionManager.instance.Explosion(PlayerPos.position);
        yield return new WaitForSeconds(0.5f);
        Features.ActiveteAll(GameEndMenuObject, GameOverTextObject);
        PauseButtonObject.SetActive(false);
        GameSceneManager.Pause();
    }
}