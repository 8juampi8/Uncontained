using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance => instance;

    int playerHealth = 3;

    GameObject defeatScreen;
    GameObject victoryScreen;
    GameObject pauseScreen;
    GameObject player;

    [SerializeField] private Guns_gun[] gunPrefabs;

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

        ReequipGun();
    }

    public void getDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            if (player != null)
                Destroy(player);

            PlayerPrefs.DeleteKey("equippedGun");

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

    public void PickGun(string gunID)
    {
        PlayerPrefs.SetString("equippedGun", gunID);
        PlayerPrefs.Save();
    }

    public void DropGun()
    {
        PlayerPrefs.DeleteKey("equippedGun");
    }

    public void ReequipGun()
    {
        string gunID = PlayerPrefs.GetString("equippedGun", "");

        if (string.IsNullOrEmpty(gunID))
            return;

        Guns_gun prefab = null;

        foreach (var gun in gunPrefabs)
        {
            if (gun.GunID == gunID)
            {
                prefab = gun;
                break;
            }
        }

        if (prefab == null)
            return;

        GameObject gunPos = GameObject.FindWithTag("GunPos");

        if (gunPos == null)
            return;

        Guns_gun newGun = Instantiate(prefab, gunPos.transform);

        newGun.transform.localPosition = Vector3.zero;
        newGun.transform.localRotation = Quaternion.identity;

        newGun.Equip();

        if (player == null)
            return;

        ItemInteraction_player item =
            player.GetComponent<ItemInteraction_player>();

        item.SetGun(newGun);
    }
}