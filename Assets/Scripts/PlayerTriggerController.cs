using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController
{
    /// <summary>
    /// to instantiate the power-up timer bar at specific canvas
    /// </summary>
    public Canvas canvas;
    public GameObject timerBar;
    private Queue<GameObject> TimerBarQueue;

    /// <summary>
    /// Defined to replace power-up timer bars sprite
    /// </summary>
    public Sprite HiddenSprite;
    public Sprite ShieldSprite;
    public Sprite TripleShotSprite;
    public Sprite X2Sprite;

    public float PowerUpWaitSceonds = 10f;

    public GameObject ShieldObj;

    public SpriteRenderer PlayerSprite;

    public GameObject TripleShuotButton;
    public GameObject FireButton;
    private void Start()
    {
        TimerBarQueue = new Queue<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CrShieldRunning && gameObject.CompareTag(Tags.Player))
        {
            if (Features.CheckTags(other.gameObject, Tags.Missile, Tags.Meteor, Tags.EnemyFire, Tags.EnemyRocket))
                TriggerEnter(other.gameObject);
        }
        // Power-Up trigger
        if (other.gameObject.CompareTag(Tags.X2PowerUp))
            StartCoroutine(X2PowerUp(other.gameObject));
        else if (other.gameObject.CompareTag(Tags.TripleShuotPowerUp))
            StartCoroutine(TripleShuotPowerUp(other.gameObject));
        else if (other.gameObject.CompareTag(Tags.HealingPowerUp))
        {
            Singleton.Instance.SetHealth(Tags.HealingPowerUp);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(Tags.ShieldPowerUp))
        {
            if (CrShieldRunning)
                StopCoroutine(CrShieldPowerUp);
            CrShieldPowerUp = StartCoroutine(ShieldPowerUp(other.gameObject));
        }
        else if (other.gameObject.CompareTag(Tags.HidePowerUp))
        {
            if (CrHideRunning)
                StopCoroutine(CrHidePowerUp);
            CrHidePowerUp = StartCoroutine(HidePowerUp(other.gameObject));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag(Tags.Player))
        {
            if (Features.CheckTags(other.gameObject, Tags.Bomb, Tags.Stones, Tags.Stones1, Tags.SmallStones, Tags.EnemyFire, Tags.EnemyRocket))
                TriggerEnter(other.gameObject);
        }
        // Power-Up trigger
        if (other.gameObject.CompareTag(Tags.X2PowerUp))
            StartCoroutine(X2PowerUp(other.gameObject));
        else if (other.gameObject.CompareTag(Tags.TripleShuotPowerUp))
            StartCoroutine(TripleShuotPowerUp(other.gameObject));
        else if (other.gameObject.CompareTag(Tags.Astronaut))
        {
            Singleton.Instance.SetHealth(Tags.Astronaut);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(Tags.ShieldPowerUp))
        {
            if (CrShieldRunning)
                StopCoroutine(CrShieldPowerUp);
            CrShieldPowerUp = StartCoroutine(ShieldPowerUp(other.gameObject));
        }
        else if (other.gameObject.CompareTag(Tags.HidePowerUp))
        {
            if (CrHideRunning)
                StopCoroutine(CrHidePowerUp);
            CrHidePowerUp = StartCoroutine(HidePowerUp(other.gameObject));
        }
    }

    float yPos = 0;
    /// <summary>
    /// It took power ups to issue a timer bar for it.
    /// </summary>
    /// <param name="sprite">Taken power up picture.</param>
    /// <returns></returns>
    IEnumerator AddTimerBar(Sprite sprite)
    {
        yield return null;

        GameObject timerBarClone = Instantiate(timerBar, canvas.transform);
        timerBarClone.transform.GetComponentInChildren<Image>().sprite = sprite;

        TimerBarQueue.Enqueue(timerBarClone);
        timerBarClone.GetComponent<RectTransform>().position -= new Vector3(0, yPos, 0);
        yPos += 125;
        yield return new WaitForSecondsRealtime(PowerUpWaitSceonds);

        Destroy(TimerBarQueue.Dequeue());
        yPos -= 125;

        if (TimerBarQueue.Count != 0 && TimerBarQueue.Peek())
        {
            for (int i = 0; i < TimerBarQueue.Count; i++)
                TimerBarQueue.ToArray()[i].GetComponent<RectTransform>().position += new Vector3(0, 125, 0);
        }
    }
    /// <summary>
    /// When X2 gets a Power Up object, score x2 is made for a short time.
    /// </summary>
    /// <param name="gameObject">X2 Power Up object</param>
    /// <returns></returns>
    IEnumerator X2PowerUp(GameObject gameObject)
    {
        yield return null;
        gameObject.SetActive(false);
        
        StartCoroutine(AddTimerBar(X2Sprite));

        Singleton.Instance.ScoreMultiplier *= 2;
        yield return new WaitForSecondsRealtime(PowerUpWaitSceonds);
        Singleton.Instance.ScoreMultiplier /= 2;
    }
    /// <summary>
    /// When triple shot power up is taken, it briefly changes from normal to triple fire and returns to normal fire after the time is up.
    /// </summary>
    /// <param name="gameObject">Triple shuot power up object</param>
    /// <returns></returns>
    IEnumerator TripleShuotPowerUp(GameObject gameObject)
    {
        yield return null;
        gameObject.SetActive(false);

        StartCoroutine(AddTimerBar(TripleShotSprite));
        TripleShuotButton.SetActive(true);
        FireButton.SetActive(false);
        yield return new WaitForSecondsRealtime(PowerUpWaitSceonds);
        TripleShuotButton.SetActive(false);
        FireButton.SetActive(true);
    }

    private Coroutine CrShieldPowerUp;
    private bool CrShieldRunning = false;
    /// <summary>
    /// Activates the shield object for a short time.
    /// </summary>
    /// <param name="gameObject">Shield power up object</param>
    /// <returns></returns>
    IEnumerator ShieldPowerUp(GameObject gameObject)
    {
        yield return null;
        gameObject.SetActive(false);
        CrShieldRunning = true;
        ShieldObj.SetActive(true);
        StartCoroutine(AddTimerBar(ShieldSprite));
        yield return new WaitForSecondsRealtime(PowerUpWaitSceonds);
        ShieldObj.SetActive(false);
        CrShieldRunning = false;
    }

    private Coroutine CrHidePowerUp;
    private bool CrHideRunning = false;
    /// <summary>
    /// When Hide receives a power up object, it hides the player for a short time, preventing them from receiving incoming hits.
    /// </summary>
    /// <param name="gameObject">Hide power up object</param>
    /// <returns></returns>
    IEnumerator HidePowerUp(GameObject gameObject)
    {
        yield return null;
        gameObject.SetActive(false);
        CrHideRunning = true;

        StartCoroutine(AddTimerBar(HiddenSprite));

        PlayerSprite.color = new Color(1f, 1f, 1f, .5f);
        this.gameObject.tag = Tags.HiddenPlayer;
        yield return new WaitForSecondsRealtime(PowerUpWaitSceonds);
        PlayerSprite.color = new Color(1f, 1f, 1f, 1f);
        this.gameObject.tag = Tags.Player;
        CrHideRunning = false;
    }
}
