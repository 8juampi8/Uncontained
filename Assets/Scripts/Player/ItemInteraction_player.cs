using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    private Item item;

    private Guns_gun slotGun;
    public Guns_gun EquippedGun => slotGun;

    private Flashlight flashlight;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    [SerializeField] private Transform gunPos;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask itemLayer;

    void Start()
    {
        flashlight = GetComponentInChildren<Flashlight>();
    }

    void Update()
    {
        DetectItem();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropGun();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.Toggle();
        }
    }

    void DetectItem()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, interactRadius, itemLayer);

        if (col == null)
        {
            item = null;
            return;
        }

        item = col.GetComponent<Item>();
    }

    void TryInteract()
    {
        if (item == null)
            return;

        EquipItem(item);
    }

    public void SetGun(Guns_gun gun)
    {
        slotGun = gun;
    }

    private void EquipItem(Item item)
    {
        GunPickup pickup = item.GetComponent<GunPickup>();

        if (pickup != null)
        {
            if (slotGun != null)
                DropGun();

            Guns_gun newGun =
                Instantiate(pickup.GunPrefab, gunPos);

            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;

            newGun.Equip();

            slotGun = newGun;

            GameManager.Instance.PickGun(newGun.GunID);

            Destroy(pickup.gameObject);

            return;
        }

        if (item.CompareTag("KeyCard"))
        {
            Destroy(item.gameObject);
            hasKeyCard = true;
        }
    }

    private void DropGun()
    {
        if (slotGun == null) return;

        Instantiate(slotGun.PickupPrefab, transform.position, transform.rotation);

        slotGun.Drop();
        slotGun = null;

        GameManager.Instance.DropGun();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}