using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


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

    private bool isEquipped = false;
    public bool IsEquipped => isEquipped;

    private Guns_gun currentGun;
    public Guns_gun CurrentGun => currentGun;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }


        instance = this;
        DontDestroyOnLoad(gameObject);


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

        SpawnItem();
    }


    public void SpawnItem()
    {
        if (string.IsNullOrEmpty(slotItem)) return;


        GameObject prefab = GetPrefab(slotItem);


        if (prefab == null) return;


        obj = Instantiate(prefab);
        currentGun = obj.GetComponent<Guns_gun>();

        hand = GameObject.FindWithTag("GunPos");


        if (hand == null) return;


        obj.transform.SetParent(hand.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = hand.transform.rotation;

        Collider2D col = obj.GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }

        isEquipped = true;
    }


    public void AddItem(string item)
    {
        slotItem = item;
    }


    public void DropItem()
    {
        if (obj != null)
        {
            Collider2D col = obj.GetComponent<Collider2D>();

            if (col != null)
            {
                col.enabled = true;
            }

            obj.transform.SetParent(null);
        }

        isEquipped = false;

        currentGun = null;
        slotItem = null;
    }


    public GameObject GetPrefab(string id)
    {
        if (itemDictionary.TryGetValue(id, out GameObject prefab))
        {
            return prefab;
        }


        return null;
    }
}
