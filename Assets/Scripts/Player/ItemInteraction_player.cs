using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    private bool onItem = false;
    private Item item;
    private Item equippedItem;
    public Item EquippedItem => equippedItem;
    private bool itemEquipped = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onItem)
        {
            item.Equip();
            equippedItem = item;

            itemEquipped = true;

        }

        if (Input.GetKeyDown(KeyCode.G) && itemEquipped)
        {
            equippedItem.Drop();

            itemEquipped = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            onItem = true;

            item = collision.gameObject.GetComponent<Item>(); ;
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