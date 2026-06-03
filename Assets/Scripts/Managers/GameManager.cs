using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance => instance;

    [SerializeField] private float flashlightPower = 100;
    public float FlashlightPower => flashlightPower;

    private int playerHealth = 3;
    public int PlayerHealth => playerHealth;

    private bool playerDied = false;
    public bool PlayerDied => playerDied;

    private int smallAmmoOD;
    public int SmallAmmoOD => smallAmmoOD;

    private int shotgunAmmoOD;
    public int ShotgunAmmoOD => shotgunAmmoOD;

    private int rifleAmmoOD;
    public int RifleAmmoOD => rifleAmmoOD;

    private float flashlightPowerOD;
    public float FlashlightPowerOD => flashlightPowerOD;

    private string gunID;
    public string GunID => gunID;

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

    private PlayerSoundsController soundsController;

    private Vector3 spawn;
    public Vector3 Spawn => spawn;

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
            if (dialogue == null)
            {
                if (pauseScreen != null)
                {
                    pauseScreen.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
            else
            {
                if (!dialogue.activeSelf)
                {
                    if (pauseScreen != null)
                    {
                        pauseScreen.SetActive(true);
                    }
                }
            }
        }

        if (pauseScreen != null)
        {
            if (!pauseScreen.activeSelf)
            {
                Time.timeScale = 1f;

                hud.SetActive(true);
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

        if (player != null)
        {
            soundsController = player.GetComponent<PlayerSoundsController>();
        }

        defeatScreen = FindAnyObjectByType<Defeat>(FindObjectsInactive.Include)?.gameObject;

        victoryScreen = FindAnyObjectByType<Victory>(FindObjectsInactive.Include)?.gameObject;

        pauseScreen = FindAnyObjectByType<Pause>(FindObjectsInactive.Include)?.gameObject;

        tutorialScreen = FindAnyObjectByType<Tutorial>(FindObjectsInactive.Include)?.gameObject;

        hud = FindAnyObjectByType<HUD>(FindObjectsInactive.Include)?.gameObject;

        dialogue = FindAnyObjectByType<Dialogue>(FindObjectsInactive.Include)?.gameObject;

        if (SceneManager.GetActiveScene().name == "Menu") flashlightPower = 100;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            player.transform.position = spawn;
        }

        smallAmmoOD = InvManager.Instance.SmallAmmo;
        shotgunAmmoOD = InvManager.Instance.ShotgunAmmo;
        rifleAmmoOD = InvManager.Instance.RifleAmmo;
        flashlightPowerOD = flashlightPower;
        gunID = InvManager.Instance.SlotItem;
    }

    public void getDamage(int damage)
    {
        soundsController.PlayOneShot(soundsController.GeneralSource, soundsController.GetDamage);

        playerHealth -= damage;
        UpdateHealth();

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            soundsController.PlayOneShot(soundsController.GeneralSource, soundsController.Death);

            playerDied = true;

            // if (player != null)
            //     Destroy(player);

            if (defeatScreen != null)
                defeatScreen.SetActive(true);

            playerHealth = 3;
        }
    }

    public void SetSpawn(Vector3 newSpawn)
    {
        spawn = newSpawn;
    }

    public void ResetDeathState()
    {
        playerDied = false;
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

        if (InvManager.Instance.Obj.GetComponent<Pistol_gun>() != null || InvManager.Instance.Obj.GetComponent<SMG_gun>())
        {
            moreAmmoTxt.text = InvManager.Instance.SmallAmmo.ToString();

            return;
        }
        if (InvManager.Instance.Obj.GetComponent<Shotgun_gun>() != null)
        {
            moreAmmoTxt.text = InvManager.Instance.ShotgunAmmo.ToString();

            return;
        }
        if (InvManager.Instance.Obj.GetComponent<Rifle_gun>() != null)
        {
            moreAmmoTxt.text = InvManager.Instance.RifleAmmo.ToString();

            return;
        }
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