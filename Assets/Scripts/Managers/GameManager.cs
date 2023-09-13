using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        instance = this;
    }
    /// <summary>
    /// Background music gameObject
    /// </summary>
    public GameObject MusicObject;
    public bool musicState = true;
    /// <summary>
    /// All background gameObjects
    /// </summary>
    public GameObject BlueBG;
    public GameObject purpleBG;
    public GameObject GreenBG;
    public GameObject BlueberryBG;
    public GameObject SpaceBG;
    public int currentBgNo;
    /// <summary>
    /// Player's state
    /// </summary>
    public string gameState = "playing";

    /// <summary>
    /// To avoid destroying this gameObject when we move to another scene
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    
}
