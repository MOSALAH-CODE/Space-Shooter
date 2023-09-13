using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 10f;
    public float timeLeft;

    /// <summary>
    /// Setting the remaining time to the maximum time
    /// </summary>
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }
    /// <summary>
    /// If there is remaining time, reduce the remaining time
    /// </summary>
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
    }
}
