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

    private int pistolAmmo = 0;
    public int PistolAmmo => pistolAmmo;

    private int shotgunAmmo = 0;
    public int ShotgunAmmo => shotgunAmmo;

    [SerializeField] private Sprite mele;
    [SerializeField] private Sprite withPistol;
    [SerializeField] private Sprite withShotgun;

    private GameObject player;
    private SpriteRenderer playerSprite;

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
        playerSprite = player.GetComponent<SpriteRenderer>();

        EquipGun();
    }

    public void AddItem(string item)
    {
        slotItem = item;
    }

    public void EquipGun()
    {
        if (string.IsNullOrEmpty(slotItem)) return;

        obj = GetPrefab(slotItem);

        if (obj == null) return;

        isEquipped = true;

        Pistol_gun gunType = obj.GetComponent<Pistol_gun>();

        if (gunType != null)
        {
            cannon = GameObject.FindWithTag("PistolCannon");
            playerSprite.sprite = withPistol;
        }
        else
        {
            cannon = GameObject.FindWithTag("ShotgunCannon");
            playerSprite.sprite = withShotgun;
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
            playerSprite.sprite = mele;
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
        pistolAmmo += 7;
        Debug.Log("Ahora tenes " + pistolAmmo + " balas de pistola en el inventario: ");
    }

    public void PickShotgunAmmo()
    {
        shotgunAmmo += 2;
        Debug.Log("Ahora tenes " + shotgunAmmo + " balas de escopeta en el inventario: ");
    }

    public void UseAmmo(int ammo)
    {
        if (obj == null) return;

        Pistol_gun pistol = obj.GetComponent<Pistol_gun>();

        if (pistol != null)
        {
            pistolAmmo -= ammo;
            Debug.Log("Balas restantes de pistola en el inventario: " + pistolAmmo);
            return;
        }

        shotgunAmmo -= ammo;
        Debug.Log("Balas restantes de escopeta en el inventario: " + shotgunAmmo);
    }
}