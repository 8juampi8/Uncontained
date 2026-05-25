using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    [SerializeField] private Flashlight flashlight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D currentItem =
                Physics2D.OverlapCircle(
                    transform.position,
                    1f,
                    itemLayer
                );

            if (currentItem == null) return;

            Item item = currentItem.GetComponent<Item>();

            if (item == null) return;

            if (item.CompareTag("Gun"))
            {
                if (InvManager.Instance.IsEquipped)
                {
                    InvManager.Instance.DropGun();
                }

                InvManager.Instance.AddItem(item.ItemName);
                InvManager.Instance.EquipGun(item.gameObject);
            }
            else
            {
                if (item.CompareTag("KeyCard"))
                {
                    hasKeyCard = true;
                }

                if (item.gameObject.CompareTag("Battery"))
                {
                    flashlight.AddPower();
                    if (flashlight == null) return;
                }

                if (item.CompareTag("PistolBullet"))
                {
                    InvManager.Instance.PickPistolAmmo();
                }
                if (item.CompareTag("ShotgunBullet"))
                {
                    InvManager.Instance.PickShotgunAmmo();
                }

                Destroy(item.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (InvManager.Instance.IsEquipped)
            {
                InvManager.Instance.DropGun();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight == null) return;

            flashlight.Toggle();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (InvManager.Instance.Obj != null) {
                InvManager.Instance.Obj.GetComponent<Guns_gun>().Reload();
                Debug.Log(InvManager.Instance.Obj.GetComponent<Guns_gun>().GunCharger);
            }
        }       

    }
}