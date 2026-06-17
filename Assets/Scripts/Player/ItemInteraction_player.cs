using UnityEngine;

public class ItemInteraction_player : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer;

    private bool hasKeyCard = false;
    public bool HasKeyCard => hasKeyCard;

    [SerializeField] private Flashlight flashlight;

    private PlayerSoundsController soundsController;

    void Start()
    {
        soundsController = gameObject.GetComponent<PlayerSoundsController>();
    }

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

                    soundsController.PlayPickAmmo();

                    break;

                case "KeyCard":
                    hasKeyCard = true;

                    soundsController.PlayPickKey();

                    break;

                case "Battery":
                    if (flashlight == null) return;
                    flashlight.AddPower();

                    soundsController.PlayPickBattery();

                    break;

                case "PistolBullet":
                    InvManager.Instance.PickPistolAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    soundsController.PlayPickAmmo();

                    break;

                case "ShotgunBullet":
                    InvManager.Instance.PickShotgunAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    soundsController.PlayPickAmmo();

                    break;
                case "RifleBullet":
                    InvManager.Instance.PickRifleAmmo();
                    GameManager.Instance.UpdateMoreAmmo();

                    soundsController.PlayPickAmmo();

                    break;
                case "Button":
                    Debug.Log("Botón presionado");
                    Button btnEvent = GameObject.FindWithTag("ButtonEvent").GetComponent<Button>();
                    Debug.Log(btnEvent);

                    btnEvent.StartCount();

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

        if (Input.GetButtonDown("Fire2"))
        {
            if (flashlight == null) return;
            flashlight.Toggle();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (InvManager.Instance.Obj != null)
            {
                InvManager.Instance.Obj.GetComponent<Guns_gun>().Reload();
            }
        }
    }
}