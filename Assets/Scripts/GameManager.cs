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

    Guns_gun equippedGun;

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

        ReequipGun();
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

            PlayerPrefs.DeleteKey("equippedGun");
            ClearWeaponVisual();

            defeatScreen.SetActive(true);
            playerHealth = 3;
        }
    }

    public void ClearWeaponVisual()
    {
        GameObject gunPos = GameObject.FindWithTag("GunPos");
        if (gunPos == null) return;

        foreach (Transform child in gunPos.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Win()
    {
        victoryScreen.SetActive(true);
    }

    public void PickGun(Guns_gun gun)
    {
        equippedGun = gun;

        PlayerPrefs.SetString("equippedGun", gun.gameObject.name);
        PlayerPrefs.Save();
    }

    public void DropGun()
    {
        equippedGun = null;

        PlayerPrefs.DeleteKey("equippedGun");
    }

    public void ReequipGun()
    {
        string gunID = PlayerPrefs.GetString("equippedGun", "");

        if (string.IsNullOrEmpty(gunID)) return;

        Guns_gun[] guns = FindObjectsByType<Guns_gun>(FindObjectsInactive.Exclude);

        foreach (var g in guns)
        {
            if (g.gameObject.name != gunID) continue;

            equippedGun = g;

            GameObject gunPos = GameObject.FindWithTag("GunPos");
            if (gunPos == null) return;

            g.transform.SetParent(gunPos.transform);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;

            g.Equip();
            return;
        }
    }
}
