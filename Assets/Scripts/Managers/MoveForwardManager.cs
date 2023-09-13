using UnityEngine;

public class MoveForwardManager : MonoBehaviour
{
    /// <summary>
    /// Object minimum and maximum speeds.
    /// </summary>
    public float speed = 4.0f;
    public float MaxSpeed = 10;
    /// <summary>
    /// Game Working status.
    /// </summary>
    public bool stopMoving = false;
    /// <summary>
    /// Object to keep time interval to increase speed and start time of each boost.
    /// </summary>
    public float WaitingTime = 10;
    private float startTime;

    public static MoveForwardManager instance = null;

    public static MoveForwardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("MoveForwardManager").AddComponent<MoveForwardManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }

    /// <summary>
    /// Object is used to increase speed over time.
    /// </summary>
    void Update()
    {
        float currTime = Time.time;
        if (currTime - startTime > WaitingTime && speed < MaxSpeed)
        {
            startTime = Time.time;
            speed++;
        }

    }
}
