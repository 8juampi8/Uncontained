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

    private bool isEquipped = false;
    public bool IsEquipped => isEquipped;

    private Guns_gun currentGun;
    public Guns_gun CurrentGun => currentGun;

    private int savedCharger = 0;
    public int SavedCharger => savedCharger;

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

        player = GameObject.FindWithTag("Player");

        if (player == null) return;

        animator = player.GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            slotItem = null;
            smallAmmo = 0;
            shotgunAmmo = 0;
            rifleAmmo = 0;
        }

        EquipGun();

        if (GameManager.Instance.PlayerDied)
        {
            smallAmmo = GameManager.Instance.SmallAmmoOD;
            shotgunAmmo = GameManager.Instance.ShotgunAmmoOD;
            rifleAmmo = GameManager.Instance.RifleAmmoOD;

            if (currentGun != null)
            {
                savedCharger = currentGun.MaxCharger;
                currentGun.setAmmo(savedCharger);
            }

            GameManager.Instance.ResetDeathState();
        }
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

        ResetWeaponBools();

        if (obj.GetComponent<Pistol_gun>() != null)
        {
            cannon = GameObject.FindWithTag("PistolCannon");
            animator.SetBool("equipPistol", true);
        }
        else if (obj.GetComponent<Shotgun_gun>() != null)
        {
            cannon = GameObject.FindWithTag("ShotgunCannon");
            animator.SetBool("equipShotgun", true);
        }
        else if (obj.GetComponent<SMG_gun>() != null)
        {
            cannon = GameObject.FindWithTag("SMGCannon");
            animator.SetBool("equipSMG", true);
        }
        else if (obj.GetComponent<Rifle_gun>() != null)
        {
            cannon = GameObject.FindWithTag("RifleCannon");
            animator.SetBool("equipRifle", true);
        }

        currentGun = obj.GetComponent<Guns_gun>();

        if (currentGun == null) return;

        savedCharger = GetGunScript(slotItem).GunCharger;

        currentGun.setCannon(cannon);
        currentGun.setAmmo(savedCharger);

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
            obj = null;

            GameManager.Instance.UpdateAmmo();
            GameManager.Instance.UpdateMoreAmmo();
        }
    }

    public void TriggerShootAnimation()
    {
        if (animator == null || obj == null) return;

        if (obj.GetComponent<Pistol_gun>() != null)
        {
            animator.SetTrigger("shootPistol");
        }
        else if (obj.GetComponent<Shotgun_gun>() != null)
        {
            animator.SetTrigger("shootShotgun");
        }
        else if (obj.GetComponent<SMG_gun>() != null)
        {
            animator.SetTrigger("shootSMG");
        }
        else if (obj.GetComponent<Rifle_gun>() != null)
        {
            animator.SetTrigger("shootRifle");
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
        Debug.Log("Ahora tenes " + smallAmmo + " balas de pistola en el inventario");
    }

    public void PickShotgunAmmo()
    {
        shotgunAmmo += 2;
        Debug.Log("Ahora tenes " + shotgunAmmo + " balas de escopeta en el inventario");
    }

    public void PickRifleAmmo()
    {
        rifleAmmo += 5;
        Debug.Log("Ahora tenes " + rifleAmmo + " balas de rifle en el inventario");
    }

    public void UseAmmo(int ammo)
    {
        if (obj == null) return;

        if (obj.GetComponent<Pistol_gun>() != null || obj.GetComponent<SMG_gun>() != null)
        {
            smallAmmo -= ammo;
            Debug.Log("Balas restantes chicas en el inventario: " + smallAmmo);
        }

        if (obj.GetComponent<Shotgun_gun>() != null)
        {
            shotgunAmmo -= ammo;
            Debug.Log("Balas restantes de escopeta en el inventario: " + shotgunAmmo);
        }

        if (obj.GetComponent<Rifle_gun>() != null)
        {
            rifleAmmo -= ammo;
            Debug.Log("Balas restantes de rifle en el inventario: " + rifleAmmo);
        }
    }
}