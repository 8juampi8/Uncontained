using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    int playerHealth = 3;

    GameObject defeatScreen;
    GameObject victoryScreen;
    GameObject pauseScreen;
    GameObject player;
    GameObject equippedGun;
    public GameObject EquippedGun => equippedGun;

    void Start()
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

        defeatScreen = FindAnyObjectByType<Defeat>(FindObjectsInactive.Include)?.gameObject;
        victoryScreen = FindAnyObjectByType<Victory>(FindObjectsInactive.Include)?.gameObject;
        pauseScreen = FindAnyObjectByType<Pause>(FindObjectsInactive.Include)?.gameObject;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
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

    public void getDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            Destroy(player);

            defeatScreen.SetActive(true);
            playerHealth = 3;
        }
    }

    public void Win()
    {
        victoryScreen.SetActive(true);
    }

    public void PickGun(GameObject gun)
    {
        equippedGun = gun;
    }

    public void DropGun()
    {
        equippedGun = null;
    }
}
