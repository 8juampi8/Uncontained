using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance => instance;

    public float flashlightPower = 10;
    int playerHealth = 3;

    GameObject defeatScreen;
    GameObject victoryScreen;
    GameObject pauseScreen;
    GameObject tutorialScreen;
    GameObject dialogue;
    GameObject player;

    [SerializeField] private Guns_gun[] gunPrefabs;

    // HUD
    private TextMeshProUGUI healthTxt;
    private TextMeshProUGUI ammoTxt;
    private TextMeshProUGUI moreAmmoTxt;
    private Slider powerSlider;

    private GameObject hud;

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
            if (dialogue.activeSelf) return;

            if (pauseScreen != null)
            {
                pauseScreen.SetActive(true);

                if (pauseScreen.activeSelf)
                {
                    hud.SetActive(false);
                    Time.timeScale = 0f;
                }
                else
                {
                    hud.SetActive(true);
                    Time.timeScale = 1f;
                }
            }
        }
        if (defeatScreen != null)
        {
            if (defeatScreen.activeSelf)
            {
                Time.timeScale = 0f;

                hud.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;

                hud.SetActive(true);
            }
        }
        if (victoryScreen != null)
        {
            if (victoryScreen.activeSelf)
            {
                Time.timeScale = 0f;

                hud.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;

                hud.SetActive(true);

            }
        }
        if (tutorialScreen != null)
        {
            if (tutorialScreen.activeSelf)
            {
                Time.timeScale = 0f;

                hud.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;

                hud.SetActive(true);

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

        tutorialScreen = FindAnyObjectByType<Tutorial>(
            FindObjectsInactive.Include)?.gameObject;

        hud = FindAnyObjectByType<HUD>(
        FindObjectsInactive.Include)?.gameObject;

        dialogue = FindAnyObjectByType<Dialogue>(
        FindObjectsInactive.Include)?.gameObject;

        if (SceneManager.GetActiveScene().name == "Level 1") flashlightPower = 100;
    }

    public void getDamage(int damage)
    {
        playerHealth -= damage;
        UpdateHealth();


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

    public void ChangePower(float value)
    {
        flashlightPower = value;
    }

    // UPDATES
    public void UpdateHealth()
    {
        if (healthTxt == null) return;

        healthTxt.text = playerHealth.ToString();
    }

    public void UpdateAmmo()
    {
        if (InvManager.Instance.Obj == null)
        {
            ammoTxt.text = "";
            return;
        }

        ammoTxt.text = InvManager.Instance.SavedCharger.ToString() + " | " + InvManager.Instance.Obj.GetComponent<Guns_gun>().MaxCharger.ToString();
    }

    public void UpdateMoreAmmo()
    {
        if (InvManager.Instance.Obj == null)
        {
            moreAmmoTxt.text = "";

            return;
        }

        Pistol_gun pistol = InvManager.Instance.Obj.GetComponent<Pistol_gun>();

        if (pistol != null)
        {
            moreAmmoTxt.text = InvManager.Instance.PistolAmmo.ToString();

            return;
        }

        moreAmmoTxt.text = InvManager.Instance.ShotgunAmmo.ToString();
    }

    public void UpdateFLpower()
    {
        if (powerSlider != null) powerSlider.value = flashlightPower;
    }

    // SAVES
    public void SaveHealth(TextMeshProUGUI health)
    {
        healthTxt = health;
        UpdateHealth();
    }

    public void SaveAmmo(TextMeshProUGUI ammo)
    {
        ammoTxt = ammo;
        UpdateAmmo();
    }

    public void SaveMoreAmmo(TextMeshProUGUI ammo)
    {
        moreAmmoTxt = ammo;
        UpdateMoreAmmo();
    }

    public void SaveFLpower(Slider power)
    {
        powerSlider = power;
        UpdateFLpower();
    }
}