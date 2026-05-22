using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteraction_player : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;
    Item item;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    [SerializeField] private Flashlight flashlight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D currentItem = Physics2D.OverlapCircle(transform.position, 1f, itemLayer);

            if (currentItem == null) return;

            Item item = currentItem.GetComponent<Item>();

            if (item == null) return;

            if (item.gameObject.CompareTag("Gun"))
            {
                if (InvManager.Instance.IsEquipped)
                {
                    InvManager.Instance.DropItem();
                }

                InvManager.Instance.AddItem(item.ItemName);
                InvManager.Instance.SpawnItem();
            }

            if (item.gameObject.CompareTag("KeyCard"))
            {
                hasKeyCard = true;
            }

            Destroy(currentItem.gameObject);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!string.IsNullOrEmpty(InvManager.Instance.SlotItem))
            {
                InvManager.Instance.DropItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight == null) return;

            flashlight.Toggle();
        }
    }

}