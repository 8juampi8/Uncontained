using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InvManager : MonoBehaviour
{
    static private InvManager instance;
    static public InvManager Instance => instance;

    private string slotItem;
    public string SlotItem => slotItem;

    [SerializeField] private List<Item> items;

    private Dictionary<string, (GameObject Prefab, Guns_gun GunScript)> itemDictionary;

    private GameObject obj;
    public GameObject Obj => obj;

    //AudioManager
    private AudioManager audioManager;
    public AudioManager Audio => audioManager;

    //Sonido de disparo
    private AudioClip shootSound;
    public AudioClip ShootSound => shootSound;

    //Si esta equipado o no
    private bool isEquipped = false;
    public bool IsEquipped => isEquipped;

    //Arma actual
    private Guns_gun currentGun;
    public Guns_gun CurrentGun => currentGun;

    //Cargador
    private int savedCharger = 0;
    public int SavedCharger => savedCharger;

    //Municiones
    private int smallAmmo = 0;
    public int SmallAmmo => smallAmmo;

    private int shotgunAmmo = 0;
    public int ShotgunAmmo => shotgunAmmo;

    private int rifleAmmo = 0;
    public int RifleAmmo => rifleAmmo;


    private Animator animator;

    private GameObject player;

    private GameObject cannon;

    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   

        itemDictionary = new Dictionary<string, (GameObject, Guns_gun)>();

        foreach (Item item in items)
        {
            if (item.Prefab != null)
            {
                Guns_gun gunComp = item.Prefab.GetComponent<Guns_gun>();

                itemDictionary[item.ItemName] = (item.Prefab, gunComp);
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
        obj = null;
        currentGun = null;
        isEquipped = false;

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            smallAmmo = 0;
            shotgunAmmo = 0;
            rifleAmmo = 0;

            slotItem = null;

            Debug.Log(slotItem);

            return;
        }

        player = GameObject.FindWithTag("Player");

        if (player == null) return;

        animator = player.GetComponent<Animator>();
        audioManager = player.GetComponent<AudioManager>();

        EquipGun();
    }

    public void ResetInvState()
    {
        smallAmmo = GameManager.Instance.SmallAmmoOD;
        shotgunAmmo = GameManager.Instance.ShotgunAmmoOD;
        rifleAmmo = GameManager.Instance.RifleAmmoOD;

        slotItem = GameManager.Instance.GunID;

        if (!string.IsNullOrEmpty(slotItem))
        {
            EquipGun();
        }

        if (currentGun != null)
        {
            savedCharger = GameManager.Instance.SavedChargerOD;
            currentGun.setAmmo(savedCharger);
        }

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
        GameManager.Instance.UpdateFLpower();
        GameManager.Instance.UpdateHealthGUI();
    }

    public void AddItem(string item)
    {
        slotItem = item;
    }

    private void ResetWeaponBools()
    {
        if (animator == null) return;
        animator.SetBool("equipPistol", false);
        animator.SetBool("equipShotgun", false);
        animator.SetBool("equipSMG", false);
        animator.SetBool("equipRifle", false);
    }

    public void EquipGun()
    {
        if (string.IsNullOrEmpty(slotItem)) return;

        obj = GetPrefab(slotItem);
        if (obj == null) return;

        isEquipped = true;
        currentGun = obj.GetComponent<Guns_gun>();

        ResetWeaponBools();

        switch (currentGun.MyType)
        {
            case Guns_gun.WeaponType.Pistol:
                cannon = GameObject.FindWithTag("PistolCannon");
                animator.SetBool("equipPistol", true);
                break;
            case Guns_gun.WeaponType.Shotgun:
                cannon = GameObject.FindWithTag("ShotgunCannon");
                animator.SetBool("equipShotgun", true);
                break;
            case Guns_gun.WeaponType.SMG:
                cannon = GameObject.FindWithTag("SMGCannon");
                animator.SetBool("equipSMG", true);
                break;
            case Guns_gun.WeaponType.Rifle:
                cannon = GameObject.FindWithTag("RifleCannon");
                animator.SetBool("equipRifle", true);
                break;
        }

        savedCharger = GetGunScript(slotItem).GunCharger;

        currentGun.setCannon(cannon);
        currentGun.setAmmo(savedCharger);
        shootSound = currentGun.ShootSound;

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
    }

    public void DropGun()
    {
        if (obj != null)
        {
            ResetWeaponBools();

            isEquipped = false;

            if (!string.IsNullOrEmpty(slotItem) && currentGun != null)
            {
                var currentSlot = itemDictionary[slotItem];
                currentSlot.GunScript.setAmmo(currentGun.GunCharger);

                itemDictionary[slotItem] = (currentSlot.Prefab, currentSlot.GunScript);
            }

            GameObject droppedItem = Instantiate(obj, player.transform.position, player.transform.rotation);
            Guns_gun droppedGunScript = droppedItem.GetComponent<Guns_gun>();

            if (droppedGunScript != null)
            {
                droppedGunScript.setAmmo(currentGun.GunCharger);
            }

            slotItem = null;
            cannon = null;
            shootSound = null;
            obj = null;
            savedCharger = 0;

            GameManager.Instance.UpdateAmmo();
            GameManager.Instance.UpdateMoreAmmo();
        }
    }

    public void TriggerShootAnimation()
    {
        if (animator == null || currentGun == null) return;

        // Usamos el mismo switch para las animaciones
        switch (currentGun.MyType)
        {
            case Guns_gun.WeaponType.Pistol: animator.SetTrigger("shootPistol"); break;
            case Guns_gun.WeaponType.Shotgun: animator.SetTrigger("shootShotgun"); break;
            case Guns_gun.WeaponType.SMG: animator.SetTrigger("shootSMG"); break;
            case Guns_gun.WeaponType.Rifle: animator.SetTrigger("shootRifle"); break;
        }
    }

    public GameObject GetPrefab(string id)
    {
        if (itemDictionary.TryGetValue(id, out var weaponData))
        {
            return weaponData.Prefab;
        }
        return null;
    }

    public Guns_gun GetGunScript(string id)
    {
        if (itemDictionary.TryGetValue(id, out var weaponData))
        {
            return weaponData.GunScript;
        }
        return null;
    }

    public void RemoveBullet()
    {
        savedCharger--;
    }

    public void AddBullet(int ammo)
    {
        savedCharger += ammo;
    }

    public void PickPistolAmmo()
    {
        smallAmmo += 7;
    }

    public void PickShotgunAmmo()
    {
        shotgunAmmo += 2;
    }

    public void PickRifleAmmo()
    {
        rifleAmmo += 3;
    }

    public void UseAmmo(int ammo)
    {
        if (obj == null) return;

        switch (currentGun.MyType)
        {
            case Guns_gun.WeaponType.Pistol:
            case Guns_gun.WeaponType.SMG:
                smallAmmo -= ammo;
                break;
            case Guns_gun.WeaponType.Shotgun:
                shotgunAmmo -= ammo;
                break;
            case Guns_gun.WeaponType.Rifle:
                rifleAmmo -= ammo;
                break;
        }
    }
}