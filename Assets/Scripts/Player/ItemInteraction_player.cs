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
            Collider2D currentItem = Physics2D.OverlapCircle(transform.position, 1.5f, itemLayer);

            if (currentItem == null) return;

            Item item = currentItem.GetComponent<Item>();

            if (item == null) return;

            switch (item.tag)
            {
                case "Gun":
                    if (InvManager.Instance.IsEquipped)
                    {
                        InvManager.Instance.DropGun();
                    }

                    // MODIFICADO: Antes de destruir el objeto del suelo, tomamos sus balas actuales
                    // y las guardamos en los datos de nuestro diccionario global
                    Guns_gun floorGunScript = item.GetComponent<Guns_gun>();
                    if (floorGunScript != null)
                    {
                        var cachedGun = InvManager.Instance.GetGunScript(item.ItemName);
                        if (cachedGun != null)
                        {
                            cachedGun.setAmmo(floorGunScript.GunCharger);
                        }
                    }

                    InvManager.Instance.AddItem(item.ItemName);
                    InvManager.Instance.EquipGun();
                    break;

                case "KeyCard":
                    hasKeyCard = true;
                    break;

                case "Battery":
                    // CORREGIDO: Se valida antes de usar la variable para evitar errores.
                    if (flashlight == null) return;
                    flashlight.AddPower();
                    break;

                case "PistolBullet":
                    InvManager.Instance.PickPistolAmmo();
                    GameManager.Instance.UpdateMoreAmmo();
                    break;

                case "ShotgunBullet":
                    InvManager.Instance.PickShotgunAmmo();
                    GameManager.Instance.UpdateMoreAmmo();
                    break;
            }

            Destroy(item.gameObject);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (InvManager.Instance.Obj != null)
            {
                InvManager.Instance.Obj.GetComponent<Guns_gun>().Reload();
                Debug.Log(InvManager.Instance.Obj.GetComponent<Guns_gun>().GunCharger);
            }
        }
    }
}