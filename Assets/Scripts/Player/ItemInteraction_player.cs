using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    private bool onItem = false;
    private Item item;

    private Guns_gun slotGun;

    public Guns_gun EquippedGun => slotGun;

    private Flashlight flashlight;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    void Start()
    {
        flashlight = GetComponentInChildren<Flashlight>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onItem)
        {
            EquipItem(item);
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

    private void EquipItem(Item item)
    {
        Guns_gun gun = item.GetComponent<Guns_gun>();

        if (gun != null)
        {
            if (slotGun)
            {
                slotGun.Drop();

                GameManager.Instance.DropGun();
            }

            GameManager.Instance.PickGun(gun);
            slotGun = gun;

            GameManager.Instance.ReequipGun();

            return;
        }

        if (item.gameObject.CompareTag("KeyCard"))
        {
            Destroy(item.gameObject);

            hasKeyCard = true;
        }
    }

    private void DropGun()
    {
        if (slotGun != null)
        {
            slotGun.Drop();
            slotGun = null;

            GameManager.Instance.DropGun();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            onItem = true;
            item = collision.gameObject.GetComponent<Item>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            onItem = false;
            item = null;
        }
    }
}