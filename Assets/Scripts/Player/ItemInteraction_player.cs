using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    private bool onItem = false;
    private Item item;

    private Lantern slotLantern;
    private Guns_gun slotGun;

    public Lantern EquippedLantern => slotLantern;
    public Guns_gun EquippedGun => slotGun;

    private bool lightOn = false;

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

        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(slotLantern);
            Debug.Log(lightOn);
            if(slotLantern != null)
            {
                if (lightOn)
                {
                    slotLantern.DisableLight();
                    lightOn = false;
                }
                if (!lightOn)
                {
                    slotLantern.EnableLight();
                    lightOn = true;
                }
            }
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
            }

            slotGun = gun;
            slotGun.Equip();

            return;
        }

        Lantern lantern = item.GetComponent<Lantern>();
        if (lantern != null)
        {
            slotLantern = lantern;
            slotLantern.Equip();

            lightOn = true;

             return;
        }
    }

    private void DropGun()
    {
        if (slotGun != null)
        {
            slotGun.Drop();
            slotGun = null;
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