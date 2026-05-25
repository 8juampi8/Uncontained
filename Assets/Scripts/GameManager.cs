using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance => instance;

    public float flashlightPower = 100;
    int playerHealth = 3;

    GameObject defeatScreen;
    GameObject victoryScreen;
    GameObject pauseScreen;
    GameObject player;

    [SerializeField] private Guns_gun[] gunPrefabs;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen != null)
            {
                pauseScreen.SetActive(true);
            }
        }
        if (victoryScreen != null && defeatScreen != null && pauseScreen != null)
        {
            if (victoryScreen.activeSelf || defeatScreen.activeSelf || pauseScreen.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");

        defeatScreen = FindAnyObjectByType<Defeat>(
            FindObjectsInactive.Include)?.gameObject;

        victoryScreen = FindAnyObjectByType<Victory>(
            FindObjectsInactive.Include)?.gameObject;

        pauseScreen = FindAnyObjectByType<Pause>(
            FindObjectsInactive.Include)?.gameObject;
    }

    public void getDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            if (player != null)
                Destroy(player);


            if (defeatScreen != null)
                defeatScreen.SetActive(true);

            playerHealth = 3;
        }
    }

    public void Win()
    {
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
    }
}