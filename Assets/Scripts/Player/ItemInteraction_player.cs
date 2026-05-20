using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    private bool onItem = false;
    private Item item;

    private Item equippedItem;
    private Guns_gun equippedGun;

    public Guns_gun EquippedGun => equippedGun;

    private bool itemEquipped = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onItem)
        {
            item.Equip();
            equippedItem = item;

            equippedGun = item.GetComponent<Guns_gun>();

            Debug.Log(equippedGun);

            itemEquipped = true;
        }

        if (Input.GetKeyDown(KeyCode.G) && itemEquipped)
        {
            equippedItem.Drop();

            equippedGun = null;
            itemEquipped = false;
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