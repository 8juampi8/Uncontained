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


    private Dictionary<string, GameObject> itemDictionary;


    private GameObject hand;

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

        itemDictionary = new Dictionary<string, GameObject>();


        foreach (var item in items)
        {
            itemDictionary[item.ItemName] = item.Prefab;
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

        SpawnGun();
    }

    public void SpawnGun()

    {
        if (string.IsNullOrEmpty(slotItem)) return;


        GameObject prefab = GetPrefab(slotItem);


        if (prefab == null) return;


        obj = Instantiate(prefab);
        currentGun = obj.GetComponent<Guns_gun>();

        if (currentGun == null) return;

        currentGun.setAmmo(savedCharger);
        isEquipped = true;

        hand = GameObject.FindWithTag("GunPos");


        if (hand == null) return;


        obj.transform.SetParent(hand.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = hand.transform.rotation;

        Collider2D col = obj.GetComponent<Collider2D>();

        if (col != null) col.enabled = false;

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
    }


    public void AddItem(string item)
    {
        slotItem = item;
    }

    public void EquipGun(GameObject item)
    {
        hand = GameObject.FindWithTag("GunPos");

        if (hand == null) return;

        item.transform.SetParent(hand.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.rotation = hand.transform.rotation;

        Collider2D col = item.GetComponent<Collider2D>();

        if (col != null) col.enabled = false;

        isEquipped = true;
        obj = item;

        Guns_gun ammo = obj.GetComponent<Guns_gun>();
        savedCharger = ammo.GunCharger;

        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
    }

    public void DropGun()
    {
        if (obj != null)
        {
            Collider2D col = obj.GetComponent<Collider2D>();

            if (col != null) col.enabled = true;




            obj.transform.SetParent(null);
        }

        isEquipped = false;

        currentGun = null;
        slotItem = null;
        obj = null;
        GameManager.Instance.UpdateAmmo();
        GameManager.Instance.UpdateMoreAmmo();
    }


    public GameObject GetPrefab(string id)
    {
        if (itemDictionary.TryGetValue(id, out GameObject prefab))
        {
            return prefab;
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